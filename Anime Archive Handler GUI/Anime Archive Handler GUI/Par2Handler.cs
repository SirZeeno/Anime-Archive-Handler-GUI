using System;
using System.Diagnostics;
using System.IO;

namespace Anime_Archive_Handler_GUI;

public static class Par2Handler
{
    private static readonly string Par2JPath = @"C:\Users\leon\AppData\Local\MultiPar\par2j64.exe";

    public static void CreatePar2(string folderPath)
    {
        // Check if the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Folder does not exist.");
            return;
        }

        // Check if the folder is empty
        if (IsFolderEmpty(folderPath))
        {
            Console.WriteLine("Folder is empty.");
            return;
        }

        // Create a list of files in the folder
        var files = Directory.GetFiles(folderPath);

        // Set the path for the PAR2 file
        var par2FilePath = Path.Combine(folderPath, "folder.par2");

        // Check if the PAR2 file already exists
        if (File.Exists(par2FilePath))
        {
            Console.WriteLine("PAR2 file already exists.");
            return;
        }

        // Set the command to create the PAR2 file for the entire folder
        var command = $"\"{Par2JPath}\" c /sm2048 /rr20 /rd1 /rf3 \"{par2FilePath}\"";

        // Append each file to the command
        foreach (var file in files) command += $" \"{file}\"";

        // Execute the command
        var exitCode = ExecuteCommand(command);

        Console.WriteLine(exitCode == 0 ? "PAR2 file created successfully." : "Error creating PAR2 file.");
    }

    private static bool IsFolderEmpty(string folderPath)
    {
        var files = Directory.GetFiles(folderPath);
        var subFolders = Directory.GetDirectories(folderPath);

        return files.Length == 0 && subFolders.Length == 0;
    }

    private static int ExecuteCommand(string command)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = new Process
        {
            StartInfo = startInfo
        };

        process.Start();
        process.StandardInput.WriteLine(command);
        process.StandardInput.Flush();
        process.StandardInput.Close();
        process.WaitForExit();

        return process.ExitCode;
    }
}