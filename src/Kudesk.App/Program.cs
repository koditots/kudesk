using Avalonia;
using System;
using Velopack;

namespace Kudesk.App;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            VelopackApp.Build().Run();
        }
        catch
        {
            try
            {
                var mgr = new UpdateManager("https://github.com/koditots/kudesk/releases");
                if (mgr.IsInstalled)
                {
                    var update = mgr.CheckForUpdatesAsync().GetAwaiter().GetResult();
                    if (update != null)
                    {
                        mgr.DownloadUpdatesAsync(update).GetAwaiter().GetResult();
                        mgr.ApplyUpdatesAndRestart(update);
                        return;
                    }
                }
            }
            catch
            {
            }
        }

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}