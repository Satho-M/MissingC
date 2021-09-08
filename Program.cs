using PlaywrightSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace MissingC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Playwright.InstallAsync("./browsers/").Wait();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Globals.browserPlaywright = new BrowserPlayWright("https://www.popmundo.com/");
            Globals.browserPlaywright.Init().Wait();
            

            frmLogin fLogin = new frmLogin();
            if (fLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new frmMain());
            }
            else
            {
                Application.Exit();
            }
        }        
    }

    static class Globals
        {
            public static BrowserPlayWright browserPlaywright;
        }
}
