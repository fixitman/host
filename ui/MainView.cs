
using System;
using Reminder_WPF.Models;
using Reminder_WPF.Services;
using Serilog;
using Terminal.Gui;

namespace host.ui;


public partial class MainView

{
    private APIReminderRepo? _repo;
    private SettingsManager<UserSettings> _setMgr;
    public UserSettings? Settings { get; set; }

    public MainView(
        SettingsManager<UserSettings> setMgr
    ){
        _setMgr = setMgr;
        InitializeComponent();  
            
        this.Loaded += Run;
    }

    private async void Run(){
       
        string token;
        Settings = await _setMgr.LoadSettingsAsync();
        LoginResponse response;
        if(Settings != null && 
            Settings.expiration != null &&
            Settings.token != null &&
            DateTime.Parse(Settings.expiration) > DateTime.Now
        ){  
            token = Settings.token;
        }else{
            response = await Login();
            token = response.token;
            await _setMgr.SaveSettingsAsync(new UserSettings{token=response.token, expiration=response.expiration});
            Log.Debug("Token = {token}  expires {exp}",response.token, response.expiration);
        }
        _repo = new APIReminderRepo(Constants.REPO_URL,token,Constants.REPO_PORT);
    }

    private async Task<LoginResponse> Login(){
        var _user = "";
        var _pass = "";
        var error = "";
        LoginResponse? response;
        do{
            var dlg = new LoginDialog(_user,_pass,error);
            Application.Run(dlg);
            response = await APIReminderRepo.GetToken(dlg.User,dlg.Password,Constants.REPO_URL,Constants.REPO_PORT);
            if(response == null) {
                error = "Incorrect Username or password";
                _user = dlg.User;
                _pass = dlg.Password;
            }
        }while(response == null);
        return response;
    }
}
