
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using host;
using host.ui;

using System.ComponentModel.DataAnnotations;
using Terminal.Gui;
using Microsoft.Extensions.Configuration;





public class Program

{
    private static readonly string LOGFILE = @"logs\TerminalApp.log";
    private static IHost _host;

    public static void Main(string[] args)
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder =>{
                builder.AddCommandLine(args);
            })
            .ConfigureServices((ctx,services) =>  {  
                services.AddTransient<MainView>();
                services.AddSingleton<SettingsManager<UserSettings>>((_) => new SettingsManager<UserSettings>(UserSettings.FILENAME));
            })
            .Build();

        //Set up Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Async(a => a.File(LOGFILE,rollingInterval:RollingInterval.Month))
            .CreateLogger();

        //Kick off the UI
        Application.Init();
        try
        {   
            Application.Run(_host.Services.GetRequiredService<MainView>());
        }
        catch (System.Exception e)
        {
            Log.Fatal("Exception: {message}",e.Message);
            throw;
        }
        finally
        {
            Application.Shutdown();
            Log.CloseAndFlush();
        }
    }

    public static IHost GetHost => _host;
}