namespace Example.WindowsFormsApp;

using System;
using System.Windows.Forms;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
#pragma warning disable CA2000
        Application.Run(new MainForm());
#pragma warning restore CA2000
    }
}
