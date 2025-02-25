
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using host;
using host.ui;





internal class Program

{
    private static readonly string LOGFILE = @"logs\TerminalApp.log";

    private static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
        {
            services.AddSingleton<TerminalApp>();    
            services.AddSingleton<MainView>();
            services.AddSingleton<SettingsManager<UserSettings>>((_) => new SettingsManager<UserSettings>(UserSettings.FILENAME));
        })
        .Build();

        //Set up Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Async(a => a.File(LOGFILE,rollingInterval:RollingInterval.Month))
            .CreateLogger();

        
        host.Services.GetRequiredService<TerminalApp>()
        .RunAsync()
        .Wait();
    }
}