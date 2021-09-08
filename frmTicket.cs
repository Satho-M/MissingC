using MetroSet_UI.Forms;
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
    public partial class frmTicket : Form
    {
        List<TicketPrice> ticketprice = new List<TicketPrice>();
        Band band;
        int idUser;
        private bool edit;

        public frmTicket(bool edit, Band band, int idUtilizador, List<TicketPrice> ticketPrice = null)
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.band = band;
            this.idUser = idUtilizador;
            this.edit = edit;
            if(edit)
                this.ticketprice = ticketPrice;

        }

        private void frmTicket_Load(object sender, EventArgs e)
        {
            if (edit)
            {
                FillScoreBoxes();
            }
        }

        private void FillScoreBoxes()
        {
            int n = 0;

            var allTextBoxes = Helper.GetChildControls<TextBox>(tableLayoutPanel1);
            IEnumerable<Control> control = allTextBoxes.OrderBy(c => c.Name);

            foreach (TextBox tb in control)
            {
                tb.Text = ticketprice[n].priceTicket;
                n++;
            }
        }

        private string CheckPop(int n)
        {
            string value;

            if (Helper.Scoring.TryGetValue(n, out value))
                return value;

            return null;

           
        }

        private void btnDoneTicket_Click(object sender, EventArgs e)
        {
            if (edit)
            {
                List<TicketPrice> temp = new List<TicketPrice>();
                int n = 0;

                var allTextBoxes = Helper.GetChildControls<TextBox>(tableLayoutPanel1);

                IEnumerable<Control> control = allTextBoxes.OrderBy(c => c.Name);

                foreach (TextBox tb in control)
                {

                    this.ticketprice[n].priceTicket = tb.Text;

                    temp.Add(ticketprice[n]);
                    n++;
                }

                SqliteDataAccess.UpdateTickets(temp);
            }
            else
            {
                List<TicketPrice> temp = new List<TicketPrice>();
                int n = 0;
                
                var allTextBoxes = Helper.GetChildControls<TextBox>(tableLayoutPanel1);

                IEnumerable<Control> control = allTextBoxes.OrderBy(c => c.Name);

                foreach (TextBox tb in control)
                {
                    TicketPrice t = new TicketPrice
                    {
                        priceTicket = tb.Text,
                        idUserTicket = idUser,
                        idBandTicket = band.idBand
                    };
                    if (!String.IsNullOrEmpty(CheckPop(n)))
                    {
                        t.popTicket = CheckPop(n);
                    }
                    
                    temp.Add(t);
                    n++;
                }

                SqliteDataAccess.SaveTickets(temp);
            }

            this.Close();
        }
    }
}
