using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.Database_Handeling;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace Anime_Archive_Handler_GUI.Interfaces;

using static LiteDbHandler;
using static CommonSettings;

public interface IEditingDatabase
{
    Task AddToDatabase<T>(T path);

    Task RemoveFromDatabase(long id);
}

public class EditAnimetoshoDb : IEditingDatabase
{
    public async Task AddToDatabase<T>(T csvFilePath)
    {
        string csvFile = csvFilePath?.ToString() ?? throw new FileNotFoundException();
        
        using var reader = new StreamReader(csvFile);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true, // If the CSV file has a header row
        };

        using var csv = new CsvReader(reader, config);

        while (await csv.ReadAsync())
        {
            try
            {
                var record = csv.GetRecord<Animetosho>();
                AnimetoshoDb.Upsert(record!);
            }
            catch (Exception? ex)
            {
                ConsoleExt.WriteLineWithPretext("Processing Record Failed! ", ConsoleExt.OutputType.Error, ex);
            }
        }

        ConsoleExt.WriteLineWithPretext("Finished importing CSV into database", ConsoleExt.OutputType.Info);
    }

    public Task RemoveFromDatabase(long id)
    {
        return Task.CompletedTask;
    }
}

public class EditNhentaiDb : IEditingDatabase
{
    public async Task AddToDatabase<T>(T directoryPath)
    {
        string dirPath = directoryPath?.ToString() ?? throw new FileNotFoundException();
        var dir = await Task.Run(() => Directory.GetDirectories(dirPath));
        // need to try and catch any exceptions
        foreach (var directory in dir)
        {
            var files = await Task.Run(() =>Directory.GetFiles(directory));
            // Gets the file with .json as the extension
            var file = files.FirstOrDefault(f => Path.GetExtension(f).Equals(".json", StringComparison.OrdinalIgnoreCase));

            if (file == null) continue;
            
            // Read the file content into a string
            var json = await File.ReadAllTextAsync(file);
            
            // Deserialize from JSON to NHentaiMetaData structure
            var metadata = JsonConvert.DeserializeObject<NHentaiMetaData>(json);
            
            if (metadata == null) continue;

            ConsoleExt.WriteLineWithPretext(metadata.id, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.nhentaiId, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.title, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.artist, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.category, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.character, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.group, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.language, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.parody, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.Pages, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.subtitle, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.tag, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.upload_date, ConsoleExt.OutputType.Info);
            ConsoleExt.WriteLineWithPretext(metadata.URL, ConsoleExt.OutputType.Info);

            //NhentaiDb.Insert(metaData);
        }
    }
    public Task RemoveFromDatabase(long id)
    {
        return Task.CompletedTask;
    }
}

// need to think about how i can use seasonNumber[] during this and how i can get it in here
public class EditAnimeList : IEditingDatabase
{
    public Task AddToDatabase<T>(T anime)
    {
        var animeDto = anime as AnimeDto;
        try
        {
            if (animeDto != null)
            {
                if (CheckAnimeExistence(animeDto.MalId))
                {
                    ConsoleExt.WriteLineWithPretext("Anime already exists in the Anime List! Skipped Anime...",
                        ConsoleExt.OutputType.Warning);
                    return Task.CompletedTask;
                }

                ToWatchListDb.Insert(animeDto);
                foreach (var titleEntry in animeDto.Titles)
                {
                    var titleEntryDb = new TitleEntryDb()
                    {
                        MalId = animeDto.MalId,
                        Title = titleEntry.Title,
                        Type = titleEntry.Type
                    };
                    ToWatchListTitlesDb.Insert(titleEntryDb);
                }

                ConsoleExt.WriteLineWithPretext("Anime Successfully added to the Anime List!", ConsoleExt.OutputType.Info);
            }
            else
            {
                ConsoleExt.WriteLineWithPretext("animeDto in while adding anime to Anime List is null!", ConsoleExt.OutputType.Error);
            }
        }
        catch (Exception? ex)
        {
            ConsoleExt.WriteLineWithPretext("Anime Failed to be added to the Anime List!", ConsoleExt.OutputType.Error, ex);
        }

        return Task.CompletedTask;
    }
    
    public Task RemoveFromDatabase(long animeId)
    {
        ToWatchListDb.DeleteMany(x => x.MalId == animeId);
        ToWatchListTitlesDb.DeleteMany(x => x.MalId == animeId);
        ConsoleExt.WriteLineWithPretext("Anime Successfully removed to the Anime List!", ConsoleExt.OutputType.Info);
        return Task.CompletedTask;
    }
}