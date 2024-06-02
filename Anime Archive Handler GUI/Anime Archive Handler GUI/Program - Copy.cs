using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FFMpegCore;
using Spectre.Console;

namespace Anime_Archive_Handler_GUI;

using static InputStringHandler;
using static DbHandler;
using static HelperClass;
using static FileHandler;
using static SettingsManager;
using static CommonSettings;

//before doing the integrity check on each episode in the folder i need to make sure the files that i am checking are actual video files and not checksum files or any other files like in the Akashic Records of Bastard Magic Instructor folder
//if the selected anime from the database is not the correct anime, create a selection of all the high similarity matches and let the user choose the correct one. But if they are still not correct, the user can choose to use a custom Name or anime
// surrealdb might me a good replacement for litedb

/// <summary>
/// The following animes are having issues in the database search process and are returning an incorrect anime or version of the anime
///
/// Yamada's First Time returns Time: Toki no Shiori
/// 
/// </summary>

/// <summary>
///     need to check the destination folder for an existing version of the anime in both languages, and if the anime is
///     dub but it finds a match in sub, it will remove the sub version, place the dub version into the dub folder. if it
///     finds the same anime in the same language it will skip it. if it finds the same anime
///     in dub but i am trying to add a sub then it will also just skip it, unless i have a setting on that will still
///     allow it to add the anime.
///     all of these checks need to be for each season of an anime and not the anime itself
///     and need the check to actually check either via a checksum or by just checking the names of the files within the
///     destination folder
/// </summary>
internal abstract partial class AnimeArchiveHandler
{
    private static readonly bool MultiplePartsInOneFolder = bool.Parse(GetSetting("Execution Settings", "MultiplePartsInOneFolder"));
    
    private static string? _animeName;
    public static string? GetAnimeName() { return _animeName;}
    public static void SetAnimeName(string? animeName) { _animeName = animeName;}
    
    private static int[]? _seasonNumbers;
    public static int[]? GetSeasonNumbers() { return _seasonNumbers;}
    public static void SetSeasonNumbers(int[]? seasonNumbers) { _seasonNumbers = seasonNumbers;}

    private static int[]? _partNumbers;
    public static int[]? GetPartNumbers() { return _partNumbers;}
    public static void SetPartNumbers(int[]? partNumbers) { _partNumbers = partNumbers;}
    
    private static string? _sourceFolder;

    private static bool _hasSubFolder;
    private static bool _hasMultipleParts;

    // need to refactor this entire starting function and check that all the features are there and de-mess things for cleaner and less boilerplate code
    private static void Start(string[] args)
    {
        InitializeProject();
        ConsoleExt.WriteLineWithPretext($"{args.Length} Selected Input Files/Folders", ConsoleExt.OutputType.Info);
        var font = FigletFont.Load(GetFileInProgramFolder(GetSetting("Execution Settings", "FigletFontFileName")));
        
        AnsiConsole.Write(
            new FigletText(font, "AAH")
                .Color(Color.Green));
        
        var programExecution = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[grey]What do you want to do?[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more Choices)[/]")
                .AddChoices("Add To Anime Collection", "Edit Anime List", "Manual DB Editing", "Scrape Animetosho Exports"));
        switch (programExecution)
        {
            case "Add To Anime YourCollection":
                if (args.Length == 0)
                {
                    string temp = GetSetting("Input Paths", "AnimeInputFolders");
                    if (temp != "null") args = temp.Split(",", StringSplitOptions.TrimEntries);
                }
                AddAnimeToCollection(args);
                break;
            case "Edit Anime List":
                AnimeListHandler.StartAnimeListEditing();
                break;
            case "Manual DB Editing":
                break;
            case "Scrape Animetosho Exports":
                Task.Run(async () => await WebScraper.ScrapeAnimetoshoExports()).GetAwaiter().GetResult();
                break;
        }
        
        ConsoleExt.WriteLineWithPretext("Program has finished running!", ConsoleExt.OutputType.Info);
        Thread.Sleep(1000000);
    }

    private static async void InitializeProject()
    {
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots2)
            .SpinnerStyle(Style.Parse("darkorange3"))
            .StartAsync("[darkorange3]Initializing...[/]", _ =>
            {
                AnsiConsole.MarkupLine("[green]Initializing FFmpeg Binaries...[/]");
                // Set the path to FFmpeg and FFProbe binaries
                GlobalFFOptions.Configure(options =>
                {
                    options.BinaryFolder = GetSetting("Program Paths", "FfmpegBinFolderPath");
                    options.TemporaryFilesFolder = "./External Dependencies/Temp";
                });
                
                AnsiConsole.MarkupLine("[green]Ensuring Database Indexes...[/]");
                EnsureIndexDb();
                
                AnsiConsole.MarkupLine("[green]Adding Required Folders...[/]");
                AddRequiredFolders();
                Console.Clear();
                
                return Task.CompletedTask;
            });
    }

    private static void AddAnimeToCollection(string[] args)
    {
        if (args.Length == 0) throw new FileNotFoundException("Input Value cannot be an empty collection. Select folder/s while opening this Program or set the source folder in UserSettings.ini", nameof(args));
        foreach (var arg in args)
        {
            _sourceFolder = arg;
            _hasSubFolder = HasSubFolders(arg);
                    
            ConsoleExt.WriteLineWithPretext($"Has sub-folders: {_hasSubFolder}", ConsoleExt.OutputType.Info);
                    
            _animeName = Task.Run(() => RemoveUnnecessaryNamePieces(new DirectoryInfo(arg).Name)).GetAwaiter().GetResult();
            var animeTitleInDb = GetAnimesWithTitle(_animeName).GetAwaiter().GetResult()!.First();

            ConsoleExt.WriteLineWithPretext(animeTitleInDb.MalId, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext($"{GetAnimeTitleWithAnime(animeTitleInDb)}, {animeTitleInDb.MalId}", ConsoleExt.OutputType.Info);

            _hasMultipleParts = HasMultipleParts(new DirectoryInfo(arg).Name);
            ExtractingSeasonNumber(new DirectoryInfo(arg).Name);
                    
            var folders = _hasSubFolder ? GetSeasonDirectories() : [arg];
                    
            foreach (var folder in folders)
            {
                var directoryFiles = Directory.GetFiles(folder); //for further use when moving the episodes
                        
                if (FileIntegrityCheck(directoryFiles))
                {
                    ExtractingLanguage(folder);
                    if (HeadlessOperations)
                    {
                        //ConsoleExt.WriteLineWithPretext("Moving All the Season Episodes!", ConsoleExt.OutputType.Info);
                        //ConsoleExt.WriteLineWithPretext("Copied all Episodes from that Season to the Anime Folder.", ConsoleExt.OutputType.Info);
                    }
                            
                    else
                    {
                        if (ManualInformationChecking())
                        {
                            //ConsoleExt.WriteLineWithPretext("Moving All the Season Episodes!", ConsoleExt.OutputType.Info);
                            //ConsoleExt.WriteLineWithPretext("Copied all Episodes from that Season to the Anime Folder.", ConsoleExt.OutputType.Info);
                        }
                    }
                }
                        
                else
                {
                    ConsoleExt.WriteLineWithPretext("Moving on to next...", ConsoleExt.OutputType.Warning);
                }
            }

            //ConsoleExt.WriteLineWithPretext($"Database Last Entre was on Line: {FindLastNonNullLine(JsonPath)}", ConsoleExt.OutputType.Info);
        }
    }
    
    //Checks if the input folder has sub-folders
    private static bool HasSubFolders(string inputDirectory)
    {
        var folderNames = Directory.GetDirectories(inputDirectory);
        return folderNames.Length > 0;
    }

    // Gets the season folders in the anime folder and nothing like movie folders, language folders
    private static string[] GetSeasonDirectories()
    {
        var allFolders = Directory.GetDirectories(_sourceFolder ?? throw new InvalidOperationException());

        foreach (var folder in allFolders)
        {
            var splitFolderName = folder.Split(@"\");
            var lastSplit = splitFolderName.Length;

            var match = MyRegex().Match(splitFolderName[lastSplit - 1]);
            if (match.Success) ConsoleExt.WriteLineWithPretext(folder, ConsoleExt.OutputType.Info);
        }

        return (from folder in allFolders
            let match = MyRegex().Match(folder.Split(@"\")[folder.Split(@"\").Length - 1])
                where match.Success
            select folder).ToArray();
    }

    // Creates all the folders if they dont already exist and is used for the folder structure that is the output and store folder for the entire program
    private static void DirectoryCreator()
    {
        if (GetSubOrDub() == null || _animeName == null || _seasonNumbers!.Length == 0)
        {
            ConsoleExt.WriteLineWithPretext($"Anime Name: {_animeName}, Sub or Dub: {GetSubOrDub().ToString()}, or Season Number: {_seasonNumbers!.Length} is null",
                ConsoleExt.OutputType.Error);
            return;
        }

        if (!Directory.Exists(AnimeOutputFolder)) Directory.CreateDirectory(AnimeOutputFolder);
        if (!Directory.Exists(Path.Combine(AnimeOutputFolder, GetSubOrDub().ToString()!)))
            Directory.CreateDirectory(Path.Combine(AnimeOutputFolder, GetSubOrDub().ToString()!));
        if (!Directory.Exists(Path.Combine(AnimeOutputFolder, GetSubOrDub().ToString()!, _animeName)))
            Directory.CreateDirectory(Path.Combine(AnimeOutputFolder, GetSubOrDub().ToString()!, _animeName));

        foreach (var season in _seasonNumbers)
        {
            if (Directory.Exists(Path.Combine(AnimeOutputFolder, GetSubOrDub().ToString()!, _animeName, @"\Season ",
                    season.ToString())))
                return;
            Directory.CreateDirectory(Path.Combine(AnimeOutputFolder, GetSubOrDub().ToString()!, _animeName, @"\Season ",
                season.ToString()));
        }
    }

    // Make a checksum from before you moved the file to after you moved it and check if they match
    // Moves all the episodes to the destination folder
    private static async Task MoveEpisodes(string[] files)
    {
        if (_seasonNumbers == null) return;
        foreach (var season in _seasonNumbers)
        {
            var episodeNumber = 1;
            foreach (var sourceFile in files)
            {
                var fileExtension = new FileInfo(sourceFile).Extension;
                if (_animeName != null)
                {
                    var destinationFile = Path.Combine(AnimeOutputFolder, GetSubOrDub().ToString()!, _animeName, "Season " + season, _animeName + " #" + episodeNumber + fileExtension);
                    if (IsValidToMove(sourceFile, destinationFile))
                    {
                        try
                        {
                            var fileInfo = new FileInfo(sourceFile);
                            var totalBytes = fileInfo.Length;
                            var chunkSizeMultiplier = int.Parse(GetSetting("Execution Settings", "FileTransferBufferChunkMultiplier"));

                            await using var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
                            await using var destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write);

                            await AnsiConsole.Progress()
                                .Start(async ctx =>
                                {
                                    // Defining the progress task
                                    var task = ctx.AddTask($"Moving [{sourceFile}]");

                                    var buffer = new byte[1024 * chunkSizeMultiplier];
                                    int bytesRead;
                                    long totalBytesRead = 0;

                                    while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                    {
                                        await destinationStream.WriteAsync(buffer, 0, bytesRead);
                                        totalBytesRead += bytesRead;
                                        task.Value = (double)totalBytesRead / totalBytes * 100;
                                    }
                                });
                        }
                        catch (Exception? ex)
                        {
                            ConsoleExt.WriteLineWithPretext($"Error moving file '{sourceFile}'", ConsoleExt.OutputType.Error, ex);
                            AnsiConsole.WriteException(ex, 
                                ExceptionFormats.ShortenPaths | ExceptionFormats.ShortenTypes |
                                ExceptionFormats.ShortenMethods | ExceptionFormats.ShowLinks);
                        }
                    }
                }

                episodeNumber++;
            }
        }
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"\d+")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
}