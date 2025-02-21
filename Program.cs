
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using host;




internal class Program

{
    private static readonly string LOGFILE = @"logs\TerminalApp.log";

    private static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<TerminalApp, TerminalApp>();        
    })
    .Build();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Async(a => a.File(LOGFILE,rollingInterval:RollingInterval.Month))
            .CreateLogger();

        host.Services.GetRequiredService<TerminalApp>().RunAsync().Wait();
    }
}