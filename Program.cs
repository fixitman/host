
using host;
using host.ui;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Terminal.Gui;
using System.Reflection;

public class Program

{
    private static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder()
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
            .ConfigureAppConfiguration(builder =>
                builder.AddCommandLine(args)
            )
            .ConfigureServices((ctx,services) => {services
                .AddTransient<MainView>()
                .AddSingleton<SettingsManager<UserSettings>>((_) => new SettingsManager<UserSettings>(UserSettings.FILENAME))
                .AddSerilog(config=> config
                    .ReadFrom.Configuration(ctx.Configuration)
                )
                .AddHostedService<StartupSvc>();
            })
            .RunConsoleAsync();
    }

    private class StartupSvc : BackgroundService
    {
        private IHost _host;
        private IHostApplicationLifetime _lifetime;
        private ILogger<StartupSvc> _logger;

        public StartupSvc(
            IHost host,
            IHostApplicationLifetime lifetime,
            ILogger<StartupSvc> logger
        ){
            _host = host;
            _lifetime = lifetime;
            _logger = logger;
        }

        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("***********************************************************");
            Task.Run( () => {
                Application.Init();
                try
                {   
                    Application.Run(_host.Services.GetRequiredService<MainView>());
                }
                catch (System.Exception e)
                {
                    _logger.LogCritical("Exception: {message}",e.Message);
                    throw;
                }
                finally
                {
                    Application.Shutdown();                    
                    _lifetime.StopApplication();
                    _logger.LogInformation("***Done***");
                }
            },stoppingToken);
            return Task.CompletedTask;
        }
    }
}