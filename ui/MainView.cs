using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace host.ui
{
using System;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Terminal.Gui;
    public partial class MainView
    {
        
        public MainView(){
            InitializeComponent();
            
            this.Loaded += Run;
        }

        private void Run(){
            Log.Information("I'm logging this");
        }
    }
}