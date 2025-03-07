
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using host;
using host.ui;
using Terminal.Gui;
using Microsoft.Extensions.Configuration;

public class Program

{
    private static void Main(string[] args)
    {
        var _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder =>
                builder.AddCommandLine(args)
            )
            .ConfigureServices((ctx,services) => services
                .AddTransient<MainView>()
                .AddSingleton<SettingsManager<UserSettings>>((_) => new SettingsManager<UserSettings>(UserSettings.FILENAME))
                .AddSerilog(config=> config
                    .ReadFrom.Configuration(ctx.Configuration)                        
                )
            )
            .Build();

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
}