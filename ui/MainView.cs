
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

        private void Run(){
            // string token;
            // LoginResponse response;
            // response = await Login();
            // token = response.token;
            // _log.LogDebug("Token = {token}  expires {exp}", response.token, response.expiration);
            // _repo = new APIReminderRepo(_config.GetValue<string>("Server:URL","UNDEFINED"),token,_config.GetValue<int>("Server:Port"));
            // var r = await _repo.GetRemindersAsync();
            // if(r.IsFailure){
            //     MessageBox.ErrorQuery("Error",$"Could not get reminders: {r.Error}","Ok");
            // }else if( r.IsEmpty){
            //     MessageBox.Query($"","No reminders yet.","Ok");
            // }else{
              //  MessageBox.Query("Success",$"{r.Value.Count} reminders retrieved.", "Ok");
                var texts = Enumerable.Range(100, 200).Select(a => a.ToString()).ToList();
                listView.Source = new ListWrapper(texts);
                listView.Height = Dim.Fill();
                
                listView.Border = new Border(){
                    BorderStyle = BorderStyle.Single,
                    BorderThickness = new Thickness(2), 
                    BorderBrush = Color.White,
                    Padding = new Thickness(5),
                    Background = Color.Magenta
                    };
                frameView.Add(listView);
                
                

                var scrollBar = new ScrollBarView(listView,true);
                scrollBar.ChangedPosition += () => {
                    listView.TopItem = scrollBar.Position;
                };

                listView.DrawContent += (s) => {
                    scrollBar.Size = listView.Source.Count;
                    scrollBar.Position = listView.TopItem;
                };

                listView.SelectedItemChanged += (e) => {
                    var item = texts[listView.SelectedItem];
                    MessageBox.Query("You chose...", item, "OK");
                };
            //}
            
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