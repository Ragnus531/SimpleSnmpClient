using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using System;

namespace SimpleSnmpClient
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            AppBuilder appBuild = null;
            try
            {
                // prepare and run your App here
                appBuild = BuildAvaloniaApp();

                appBuild.StartWithClassicDesktopLifetime(args);
            }
            catch (Exception e)
            {
                // here we can work with the exception, for example add it to our log file
                //Log.Fatal(e, "Something very bad happened");
                Console.WriteLine("dsada");
                appBuild = null;
                appBuild.StartWithClassicDesktopLifetime(args);
            }
            finally
            {
                // This block is optional. 
                // Use the finally-block if you need to clean things up or similar
                //Log.CloseAndFlush();
                //Console.WriteLine("dsdas");
                //BuildAvaloniaApp()
                //  .StartWithClassicDesktopLifetime(args);
            }
            
        }
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}
