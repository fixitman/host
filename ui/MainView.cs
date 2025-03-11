
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reminder_WPF.Models;
using Reminder_WPF.Services;
using Reminder_WPF.Utilities;
using Terminal.Gui;

namespace host.ui {

    public partial class MainView 
    {
        private APIReminderRepo? _repo;
        private IConfiguration _config;
        private ILogger<MainView> _log {get;}

        public MainView(
            IConfiguration configuration,
            ILogger<MainView> logger
        ){
            _log = logger;
            _config = configuration;
            InitializeComponent();
            listView.SetSource(new List<string>());
            this.Loaded += Run;
        }

        private async void Run(){
            string token;
            LoginResponse response;
            response = await Login();
            token = response.token;
            _log.LogDebug("Token = {token}  expires {exp}", response.token, response.expiration);
            _repo = new APIReminderRepo(_config.GetValue<string>("Server:URL","UNDEFINED"),token,_config.GetValue<int>("Server:Port"));
            var r = await _repo.GetRemindersAsync();
            if(r.IsFailure){
                MessageBox.ErrorQuery("Error",$"Could not get reminders: {r.Error}","Ok");
            }else if( r.IsEmpty){
                MessageBox.Query($"","No reminders yet.","Ok");
            }else{
                MessageBox.Query("Success",$"{r.Value.Count} reminders retrieved.", "Ok");
                var texts = r.Value.Select(e => e.ReminderText).ToList();
                var widest = texts.Max(t => t.Length);
                listView.Height = texts.Count;
                listView.Width = widest + 2;
                listView.SetSource(texts);
            }
            
        }

        private async Task<LoginResponse> Login(){
            var _user = "";
            var _pass = "";
            var error = "";
            Result<LoginResponse?> result;
            do
            {
                var dlg = new LoginDialog(_user, _pass, error);               
                Application.Run(dlg);
                result = await APIReminderRepo
                    .GetToken(dlg.User, dlg.Password, _config.GetValue<string>("Server:URL", "NotDefined"), _config.GetValue<int>("Server:Port"));
                if (!result.IsFailureOrEmpty)
                {
                    return result.Value!;
                }
                error = $"{result.Error}";
                _user = dlg.User;
                _pass = dlg.Password;
            } while (true);            
        }
    }
}