
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.1.0.0
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace host.ui {
    using System;
    using Terminal.Gui;
    
    
    public partial class LoginDialog : Terminal.Gui.Dialog {
        
        private Terminal.Gui.ColorScheme redOnBlack;
        
        private Terminal.Gui.ColorScheme error;
        
        private Terminal.Gui.Label lblUser;
        
        private Terminal.Gui.TextField txtUser;
        
        private Terminal.Gui.Label lblPass;
        
        private Terminal.Gui.TextField txtPass;
        
        private Terminal.Gui.Button btnOk;
        
        private Terminal.Gui.Label lblError;
        
        private void InitializeComponent() {
            this.lblError = new Terminal.Gui.Label();
            this.btnOk = new Terminal.Gui.Button();
            this.txtPass = new Terminal.Gui.TextField();
            this.lblPass = new Terminal.Gui.Label();
            this.txtUser = new Terminal.Gui.TextField();
            this.lblUser = new Terminal.Gui.Label();
            this.redOnBlack = new Terminal.Gui.ColorScheme();
            this.redOnBlack.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Red, Terminal.Gui.Color.Black);
            this.redOnBlack.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Black);
            this.redOnBlack.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Red, Terminal.Gui.Color.Brown);
            this.redOnBlack.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Brown);
            this.redOnBlack.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.Gray, Terminal.Gui.Color.Black);
            this.error = new Terminal.Gui.ColorScheme();
            this.error.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.Red);
            this.error.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.White, Terminal.Gui.Color.Black);
            this.error.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.White, Terminal.Gui.Color.Black);
            this.error.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.White, Terminal.Gui.Color.Black);
            this.error.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.White, Terminal.Gui.Color.Black);
            this.Width = Dim.Percent(60f);
            this.Height = Dim.Percent(60f);
            this.X = 24;
            this.Y = 6;
            this.Visible = true;
            this.Modal = true;
            this.IsMdiContainer = false;
            this.Border.BorderStyle = Terminal.Gui.BorderStyle.Single;
            this.Border.Effect3D = true;
            this.Border.Effect3DBrush = null;
            this.Border.DrawMarginFrame = true;
            this.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Title = "Please Log In";
            this.lblUser.Width = 4;
            this.lblUser.Height = 1;
            this.lblUser.X = 20;
            this.lblUser.Y = Pos.Percent(25f);
            this.lblUser.Visible = true;
            this.lblUser.Data = "lblUser";
            this.lblUser.Text = "Username: ";
            this.lblUser.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.lblUser);
            this.txtUser.Width = 20;
            this.txtUser.Height = 1;
            this.txtUser.X = Pos.Right(lblUser) + 1;
            this.txtUser.Y = Pos.Top(lblUser);
            this.txtUser.Visible = true;
            this.txtUser.Secret = false;
            this.txtUser.Data = "txtUser";
            this.txtUser.Text = "";
            this.txtUser.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.txtUser);
            this.lblPass.Width = 4;
            this.lblPass.Height = 1;
            this.lblPass.X = Pos.Left(lblUser);
            this.lblPass.Y = Pos.Percent(50f);
            this.lblPass.Visible = true;
            this.lblPass.Data = "lblPass";
            this.lblPass.Text = "Password:";
            this.lblPass.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.lblPass);
            this.txtPass.Width = 20;
            this.txtPass.Height = 1;
            this.txtPass.X = Pos.Left(txtUser);
            this.txtPass.Y = Pos.Top(lblPass);
            this.txtPass.Visible = true;
            this.txtPass.Secret = true;
            this.txtPass.Data = "txtPass";
            this.txtPass.Text = "";
            this.txtPass.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.txtPass);
            this.btnOk.Width = 6;
            this.btnOk.Height = 1;
            this.btnOk.X = Pos.Center();
            this.btnOk.Y = Pos.Percent(75f);
            this.btnOk.Visible = true;
            this.btnOk.Data = "btnOk";
            this.btnOk.Text = "OK";
            this.btnOk.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.btnOk.IsDefault = true;
            this.Add(this.btnOk);
            this.lblError.Width = Dim.Fill(1);
            this.lblError.Height = 1;
            this.lblError.X = 1;
            this.lblError.Y = Pos.Percent(100f) - 1;
            this.lblError.Visible = true;
            this.lblError.ColorScheme = this.error;
            this.lblError.Data = "lblError";
            this.lblError.Text = "";
            this.lblError.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.Add(this.lblError);
        }
    }
}
