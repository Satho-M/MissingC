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
    public partial class frmTicket : MetroSetForm
    {
        private SortedDictionary<int, string> Scoring = new SortedDictionary<int, string>
            { { 0, "truly abysmal" }, { 1, "abysmal" }, { 2, "bottom dwelling" }, { 3, "horrendous" }, { 4, "dreadful" }, { 5, "terrible" },
            { 6, "poor" }, { 7, "below average" }, { 8, "mediocre" }, { 9, "above average" }, { 10, "decent" }, { 11, "nice" }, { 12, "pleasant" },
            { 13, "good" }, { 14, "sweet" }, { 15, "splendid" }, { 16, "awesome" }, { 17, "great" }, { 18, "terrific" }, { 19, "wonderful" },
            { 20, "incredible" }, { 21, "perfect" }, { 22, "revolutionary" }, { 23, "mind melting" }, { 24, "earth shaking" }, { 25, "GOD SMACKING" }, { 26, "GOD SMACKINGLY GLORIOUS" } };
        List<TicketPrice> ticketprice = new List<TicketPrice>();
        Band band;
        string idUser;
        private bool edit;

        public frmTicket(bool edit, Band band, string idUtilizador, List<TicketPrice> ticketPrice = null)
        {
            InitializeComponent();
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

            this.labelBand.Text = "Band: " + band.Name;
        }

        private void FillScoreBoxes()
        {
            int n = 0;

            var allTextBoxes = Helper.GetChildControls<TextBox>(tableLayoutPanel1);
            IEnumerable<Control> control = allTextBoxes.OrderBy(c => c.Name);

            foreach (TextBox tb in control)
            {
                tb.Text = ticketprice[n].Price;
                n++;
            }
        }

        private string CheckPop(int n)
        {
            string value;

            if (this.Scoring.TryGetValue(n, out value))
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

                    this.ticketprice[n].Price = tb.Text;

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
                        Price = tb.Text,
                        UserId = idUser,
                        BandId = band.Id
                    };
                    if (!String.IsNullOrEmpty(CheckPop(n)))
                    {
                        t.Popularity = CheckPop(n);
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
