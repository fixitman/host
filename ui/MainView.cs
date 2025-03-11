
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Reminder_WPF.Models;
using Reminder_WPF.Services;
using Reminder_WPF.Utilities;
using Serilog;
using Terminal.Gui;

namespace host.ui {


    public partial class MainView 
    {
        private APIReminderRepo? _repo;
        private IConfiguration _config;
        private SettingsManager<UserSettings> _setMgr;
        private ILogger<MainView> _log {get;}
        private IHost _host;

        public MainView(
            SettingsManager<UserSettings> setMgr,
            IConfiguration configuration,
            IHost host,
            ILogger<MainView> logger
        ){
            _setMgr = setMgr;
            _log = logger;
            _config = configuration;
            _host = host;
            InitializeComponent();
            this.Loaded += Run;
        }

        private async void Run(){
            string token;
            LoginResponse response;
            response = await Login();
            token = response.token;
            _log.LogDebug("Token = {token}  expires {exp}", response.token, response.expiration);
            _repo = new APIReminderRepo(_config.GetValue<string>("Server:URL","UNDEFINED"),token,_config.GetValue<int>("Server:Port"));
        }

        private async Task<LoginResponse> Login(){
            var _user = "";
            var _pass = "";
            var error = "";
            Result<LoginResponse?> response;
            do
            {
                var dlg = new LoginDialog(_user, _pass, error);               
                Application.Run(dlg);
                response = await APIReminderRepo.GetToken(dlg.User, dlg.Password, _config.GetValue<string>("Server:URL", "NotDefined"), _config.GetValue<int>("Server:Port"));
                if (!response.IsFailureOrEmpty)
                {
                    return response.Value!;
                }
                error = $"{response.Error}";
                _user = dlg.User;
                _pass = dlg.Password;
            } while (true);            
        }
    }
}