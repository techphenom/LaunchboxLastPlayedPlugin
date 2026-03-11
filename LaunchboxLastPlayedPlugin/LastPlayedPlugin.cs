using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace LaunchboxLastPlayedPlugin;

public class LastPlayedPlugin : IGameLaunchingPlugin
{
    
    public void OnBeforeGameLaunching(IGame game, IAdditionalApplication app, IEmulator emulator)
    {
        string rootPath = Directory.GetCurrentDirectory();
        string scriptsPath = Path.Combine(rootPath, "Scripts");
        string batFilePath = Path.Combine(scriptsPath, "last_played.bat");
        string logFilePath = Path.Combine(scriptsPath, "last_played.log");

        string gameFolder = Path.Combine(rootPath, game.RootFolder);
        
        string launchCommand;
        
        if (!Directory.Exists(scriptsPath))
        {
            Directory.CreateDirectory(scriptsPath);
        }

        CleanupLog(logFilePath);

        if (emulator != null)
        {
            // EMULATED GAME: "C:\Path\To\Emulator.exe" [args] "C:\Path\To\Game.rom"
            string exePath = $"\"{emulator.ApplicationPath}\"";
            string gamePath = $"\"{game.ApplicationPath}\"";
            string emuArgs = emulator.CommandLine ?? "";
            string gameArgs = game.CommandLine ?? "";
            launchCommand = $"{exePath} {emuArgs} {gameArgs} {gamePath}";
        }
        else if (game.ApplicationPath.Contains("://"))
        {
            launchCommand = $"start \"\" \"{game.ApplicationPath}\"";
        }
        else
        {
            launchCommand = $"start \"\" \"{Path.GetFileName(game.ApplicationPath)}\" {game.CommandLine}";
        }

        string content = $"@echo off\n" +
                         $"cd /d \"{gameFolder}\"\n" +
                         $"{launchCommand}";

        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Launched: {game.Title} | Command: {launchCommand}{Environment.NewLine}";

        try 
        {
            File.WriteAllText(batFilePath, content);
            File.AppendAllText(logFilePath, logEntry);
        }
        catch (System.Exception ex)
        {
            string logError = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR | Command Attempted: {launchCommand}{Environment.NewLine}";
            File.AppendAllText(logFilePath, logError);
        }
    }

    private void CleanupLog(string logFilePath)
    {
        DateTime cutoffDate = DateTime.Now.AddYears(-1);
        
        if (File.Exists(logFilePath))
        {
            try
            {
                var lines = File.ReadAllLines(logFilePath);
                var recentLines = lines.Where(line =>
                {
                    if (line.StartsWith("[") && line.Length > 20)
                    {
                        string datePart = line.Substring(1, 19); // Extract yyyy-MM-dd HH:mm:ss
                        if (DateTime.TryParse(datePart, out DateTime logDate))
                        {
                            return logDate >= cutoffDate;
                        }
                    }
                    return true;
                }).ToList();

                File.WriteAllLines(logFilePath, recentLines);
            }
            catch { /*  */ }
        }
    }
    
    public void OnAfterGameLaunched(IGame game, IAdditionalApplication app, IEmulator emulator) { }

    public void OnGameExited() { }
}
