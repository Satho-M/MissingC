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
    public partial class frmCharacter : Form
    {
        public frmCharacter()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        private async void frmCharacter_Load(object sender, EventArgs e)
        {
            List<Character> characters = await Globals.browserPlaywright.GetCharacters();
            FillTableCharacter(characters);
        }

        private void FillTableCharacter(List<Character> characters)
        {
            var x = 30;
            var y = 0;

            foreach(Character character in characters)
            {
                var groupBox = new GroupBox() { Name = character.Name, Location = new System.Drawing.Point(PanelMain.Width/4, y), AutoSize = true };

                y += 40;
                
                var label = new Label() { Text = character.Name, Location = new System.Drawing.Point(x, y), AutoSize = true};
                var button = new Button() { Text = character.ID.ToString(), Location = new System.Drawing.Point(label.Location.X + 150, label.Location.Y), AutoSize = true};
                
                
                groupBox.Controls.Add(label);
                groupBox.Controls.Add(button);

                PanelMain.Controls.Add(groupBox);

                button.Click += new System.EventHandler(ButtonClick);
            }
        }

        private async void ButtonClick(object sender, EventArgs e)
        {
            try 
            {
                var s = (Button)sender;
                var ID = s.Text;
                await Globals.browserPlaywright.ChooseCharacter(ID);

                this.Close();
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
