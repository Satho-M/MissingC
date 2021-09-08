using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissingC
{
    public partial class frmLogin : Form
    {
        
        public frmLogin()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(600, 560);
        }

        private async void btnLogin_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                string username = txtBoxUsername.Text;
                string password = txtBoxPassword.Text;

                switch (await Globals.browserPlaywright.Login(username, password))
                {
                    case Status.Success:
                        this.Close();
                        DialogResult = DialogResult.OK;
                        break;
                    case Status.Failed:
                        MessageBox.Show("Wrong username or password.");
                        break;
                    case Status.WrongURL:
                        await Globals.browserPlaywright.GoTo("https://www.popmundo.com/");
                        MessageBox.Show("Please try to log in again.");
                        break;

                }
            }
            catch (PlaywrightSharp.PlaywrightSharpException err)
            {
                if (err.Message.Contains("DISCONNECTED"))
                {
                    MessageBox.Show("Error: Check your connection and try again.");
                }
            }
        }
    }
}
