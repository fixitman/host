using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using host.ui;
using Microsoft.Extensions.Logging;
using Terminal.Gui;
using Serilog;
using Reminder_WPF.Services;


namespace host
{
    public class TerminalApp()
    {
        
        public async Task RunAsync(){
            await Task.Run(()=>{                
                //Initialize UI
                Application.Init();
                try
                {
                    Application.Run(new MainView());
                }
                catch (System.Exception e)
                {
                    Log.Fatal("Exception: {message}",e.Message);
                }
                finally
                {
                    Application.Shutdown();
                    Log.CloseAndFlush();
                }
            });
        } 
    }
}