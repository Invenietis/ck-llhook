using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ViewHookControls;

namespace ViewHookAnyCPU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            var w = new MainWindow();
            w.Text += " - (Any CPU executable)";
            Application.Run( w );
        }
    }
}
