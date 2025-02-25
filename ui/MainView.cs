
using System;
using Reminder_WPF.Models;
using Reminder_WPF.Services;
using Serilog;
using Terminal.Gui;

namespace host.ui;


public partial class MainView

{
    private APIReminderRepo? _repo;
    
    public MainView(){
        InitializeComponent();  
            
        this.Loaded += Run;
    }

    private async void Run(){
        SettingsManager<UserSettings> setMgr = new (UserSettings.FILENAME);
        var settings = setMgr.LoadSettings();
        string token = "";
        LoginResponse? response;
        if(settings != null && 
            settings.expiration != null &&
            settings.token != null &&
            DateTime.Parse(settings.expiration) <= DateTime.Now
        ){  
            token = settings.token;
        }else{
            response = await Login();
            if(response !=null){
                token = response.token;
                setMgr.SaveSettings(new UserSettings{token=response.token, expiration=response.expiration});
                Log.Debug("Token = {token}  expires {exp}",response.token, response.expiration);
            }else{
                Application.RequestStop();
            }
        }
        _repo = new APIReminderRepo(Constants.REPO_URL,token,Constants.REPO_PORT);
    }

    private async Task<LoginResponse> Login(){
        var _user = "";
        var _pass = "";
        var error = "";
        LoginResponse? token;
        do{
            var dlg = new LoginDialog(_user,_pass,error);
            Application.Run(dlg);
            token = await APIReminderRepo.GetToken(dlg.User,dlg.Password,Constants.REPO_URL,Constants.REPO_PORT);
            if(token == null) {
                error = "Incorrect Username or password";
                _user = dlg.User;
                _pass = dlg.Password;
            }
        }while(token == null);
        return token;
    }
}
