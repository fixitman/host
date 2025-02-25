using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace host.ui
{
using System;
    using System.Reflection.Metadata;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Reminder_WPF.Models;
    using Reminder_WPF.Services;
    using Serilog;
    using Terminal.Gui;
    public partial class MainView
    {
        private APIReminderRepo? _repo;
        
        public MainView(){
            InitializeComponent();  
               
            this.Loaded += Run;
        }

        private async void Run(){
            var response = await Login();
            if(response !=null){
                _repo = new APIReminderRepo(Constants.REPO_URL,response.token,Constants.REPO_PORT);
                Log.Information("Token = {token}",response);
            }
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
}