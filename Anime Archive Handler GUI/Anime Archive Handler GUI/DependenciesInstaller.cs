using System.Diagnostics;

namespace Anime_Archive_Handler_GUI;

public class DependenciesInstaller
{
    public void DependenciesInstallWizard()
    {
        bool IsGitInstalled(out string? version)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "git",
                        Arguments = "--version",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                version = process.StandardOutput.ReadToEnd().Trim();
                process.WaitForExit();

                return process.ExitCode == 0;
            }
            catch
            {
                version = null;
                return false;
            }
        }
    }
}