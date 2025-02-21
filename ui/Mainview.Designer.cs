
using Terminal.Gui;
using System;

namespace host.ui;
public partial class MainView : Window
{

    private Terminal.Gui.Label label;

    private void InitializeComponent(){
        this.label = new Label();
        this.Title = "Welcome";
        this.X = 0;
        this.Y = 0;
        this.Width = Dim.Fill(0);
        this.Height = Dim.Fill(0);
        this.Border.BorderStyle = BorderStyle.Rounded;
        this.label.Text = "Hello, World!";
        this.label.X = Pos.Center();
        this.label.Y = Pos.Center();
        this.Add(label);
    }
}
