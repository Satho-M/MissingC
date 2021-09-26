using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using PlaywrightSharp;
using System.Globalization;

namespace MissingC
{
    public partial class frmMain : Form
    {
        bool formClosing = false;

        public frmMain()
        {   
            InitializeComponent();

            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(600, 560);

            this.FormClosing += new FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new EventHandler(this.frmMain_Load);
            checkedListBoxChains.DisplayMember = "nameChain";
            checkedListBoxBands.DisplayMember = "nameBand";

            if(Globals.browserPlaywright.CheckURLContains("/ChooseCharacter"))
            {
                //Opens Form for Character Selection
                frmCharacter character = new frmCharacter();
                character.ShowDialog();
            }


        }
        private async void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!formClosing) {
                if (MessageBox.Show("Would you like to close the client?", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    formClosing = true;
                    this.Hide();
                    e.Cancel = true;

                    await Globals.browserPlaywright.ChangeGameLanguage(Globals.browserPlaywright.userLanguage);
                    this.Close();
                }
            }
        }
        private async void frmMain_Load(object sender, EventArgs e)
        {
            //Gathers all the information necessary.
            //Changes language to English UK.
            await InitializeProgramAsync();

            //Fills the ComboBox of Tour Years
            Console.WriteLine(Globals.browserPlaywright.gameYear);
            FillYearList();

        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            //Fills the ListBox of Chains at the start of the program (From the database)
            FillListBoxChains();
        }

        private async Task InitializeProgramAsync()
        {
            try {
                await Globals.browserPlaywright.GetServer();
                Console.WriteLine("Current server: " + Globals.browserPlaywright.serverURL);
            
                if(Globals.browserPlaywright.userID == -1)
                    await Globals.browserPlaywright.GetUserID();

                await Globals.browserPlaywright.GetCurrentGameLanguage();

                await Globals.browserPlaywright.ChangeGameLanguage();            

                CheckUser(Globals.browserPlaywright.userID);

                await Globals.browserPlaywright.GetCurrentGameYear();
            }
            catch (PlaywrightSharpException err)
            {
                if (err.Message.Contains("DISCONNECTED"))
                {
                    MessageBox.Show("Error: Check your connection and try again.");
                }
            }
        }
        private void LabelUpdate(string status = "Idle")
        {
            var threadParameters = new System.Threading.ThreadStart(delegate { UpdateLabelSafely(status); });
            var thread2 = new System.Threading.Thread(threadParameters);
            thread2.Start();
        }
        public void UpdateLabelSafely(string text)
        {
            if (this.labelStatus.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { UpdateLabelSafely($" {text}"); };
                this.labelStatus.Invoke(safeWrite);
            }
            else
                this.labelStatus.Text = text;
        }
        private void CheckUser(int id)
        {
            LabelUpdate("Checking User");

            if (!SqliteDataAccess.LoadUser(id))
            {
                SqliteDataAccess.SaveUser(id);
            }

            LabelUpdate();
        }
        private List<int> CreateYearListForDataSource(string year)
        {
            List<int> list = new List<int>();

            if (Int32.TryParse(year, out int intYear))
            {
                for (int i = 0; i < 6; i++)
                {
                    int temp = intYear - 2 + i;
                    list.Add(temp);
                }
            }
            return list;
        }
        private void EnableOrDisableButtons(bool status)
        {
            foreach(Control ctrls in this.Controls)
            {
                foreach(Control ctrl in ctrls.Controls)
                {
                    if(ctrl is Button)
                        ctrl.Enabled = status;
                }
            }
        }

        //Retrieve Clubs from Game
        private async void btnUpdateClubs_Click(object sender, EventArgs e)
        {
            EnableOrDisableButtons(false);

            try {

                LabelUpdate("Retrieving Clubs");
                var Clubs = await Globals.browserPlaywright.GetClubs();
            

                SqliteDataAccess.SaveClubs(CompareClubsDBIn(Clubs));

                SqliteDataAccess.DeleteClubs(CompareClubsDBOut(Clubs));

                EnableOrDisableButtons(true);

                LabelUpdate();
            }
            catch (PlaywrightSharpException err)
            {
                if (err.Message.Contains("DISCONNECTED"))
                {
                    MessageBox.Show("Error: Check your connection and try again.");
                }
            }
        }
        private List<Club> CompareClubsDBIn(List<Club> input) //Compares the clubs the user has in-game with the ones in the database
        {
            LabelUpdate("Adding new clubs");

            List<Club> dbClubs = new List<Club>(SqliteDataAccess.LoadClubs(Globals.browserPlaywright.userID));

            List<Club> output = input.Where(n => !dbClubs.Select(n1 => n1.idClub).Contains(n.idClub)).ToList();


            LabelUpdate();

            return output;
        }
        private List<Club> CompareClubsDBOut(List<Club> input) //Compares the clubs the user has in the database with the ones in-game 
        {
            LabelUpdate("Deleting missing clubs.");
            List<Club> dbClubs = new List<Club>(SqliteDataAccess.LoadClubs(Globals.browserPlaywright.userID));

            //HashSet<Club> hashset = new HashSet<Club>(input);
            //IEnumerable<Club> nonduplicates = dbClubs.Where(e => hashset.Add(e));

            //List<Club> output = new List<Club>(nonduplicates);

            List<Club> output = dbClubs.Where(n => !input.Select(n1 => n1.idClub).Contains(n.idClub)).ToList();

            LabelUpdate();

            return output;
        }

        //Chains
        private void checkedListBoxChains_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxChains.Items.Count > 0)
                FillListBands();
        }
        private void FillListBoxChains()
        {
            checkedListBoxChains.DataSource = SqliteDataAccess.LoadChains(Globals.browserPlaywright.userID);
        }
        private void btnNewChain_Click(object sender, EventArgs e)
        {
            Form formChain = new frmChain(false, Globals.browserPlaywright.userID);

            formChain.ShowDialog();

            FillListBoxChains();
        }
        private void btnEditChain_Click(object sender, EventArgs e)
        {
            if(checkedListBoxChains.SelectedIndex > -1)
            {
                Form formChain = new frmChain(true, Globals.browserPlaywright.userID, checkedListBoxChains.SelectedItem as Chain);

                formChain.ShowDialog();

                FillListBoxChains();
            }
        }
        private void btnDeleteChain_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty((checkedListBoxChains.SelectedItem as Chain).idChain.ToString()))
            {
                SqliteDataAccess.DeleteChain((checkedListBoxChains.SelectedItem as Chain).idChain);

                FillListBoxChains();
            }
        }

        //Bands
        private void checkedListBoxBands_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillYearList();
        }
        private void FillListBands()
        {
            if (checkedListBoxChains.SelectedIndex > -1)
                checkedListBoxBands.DataSource = SqliteDataAccess.LoadBands(checkedListBoxChains.SelectedItem as Chain);
            else
                checkedListBoxBands.Items.Clear();
        }
        private void btnNewBand_Click(object sender, EventArgs e)
        {
            if (checkedListBoxChains.Items.Count != 0)
            {
                Form formBand = new frmBand(
                false,
                Globals.browserPlaywright.userID,
                checkedListBoxChains.SelectedItem as Chain);

                formBand.ShowDialog();
                FillListBands();
            }
            else
            {
                MessageBox.Show("Please create a Chain before creating a Band.");
            }
        }
        private void btnEditBand_Click(object sender, EventArgs e)
        {
            Form formBand = new frmBand(
                true,
                Globals.browserPlaywright.userID,
                checkedListBoxChains.SelectedItem as Chain,
                checkedListBoxBands.SelectedItem as Band);

            formBand.ShowDialog();

            FillListBands();
        }
        private void btnDeleteBand_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.DeleteBand((checkedListBoxBands.SelectedItem as Band).idBand);

            FillListBands();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.ResetLastInviteBand((checkedListBoxBands.SelectedItem as Band));
        }

        private void btnEditTicket_Click(object sender, EventArgs e)
        {
            bool edit;

            edit = SqliteDataAccess.CheckTicketBand(checkedListBoxBands.SelectedItem as Band);
            List<TicketPrice> tickets;

            if (edit)
            {
                tickets = SqliteDataAccess.GetTicketsBand(checkedListBoxBands.SelectedItem as Band);

                Form formTicket = new frmTicket(
                    edit,
                    checkedListBoxBands.SelectedItem as Band,
                    Globals.browserPlaywright.userID,
                    tickets);

                formTicket.ShowDialog();
            }
            else
            {
                Form formTicket = new frmTicket(edit,
                    checkedListBoxBands.SelectedItem as Band,
                    Globals.browserPlaywright.userID);

                formTicket.ShowDialog();
            }
        }
        private void btnEditTour_Click(object sender, EventArgs e)
        {
            int year = Int32.Parse(cmbBoxPPMYear.SelectedItem.ToString());

            bool edit = SqliteDataAccess.CheckTour(
                year,
                (checkedListBoxBands.SelectedItem as Band).idBand,
                Globals.browserPlaywright.userID);

            Form formTour = new frmTouring(
                year, 
                edit, 
                Globals.browserPlaywright.userID,
                checkedListBoxBands.SelectedItem as Band);

            formTour.ShowDialog();
        }
        private void FillYearList()
        {
            cmbBoxPPMYear.DataSource = CreateYearListForDataSource(Globals.browserPlaywright.gameYear);
            ControlInvokeExtensions.InvokeOnHostThread(cmbBoxPPMYear, Refresh);
        }

        private async void btnSendInvites_Click(object sender, EventArgs e)
        {
            try {
                EnableOrDisableButtons(false);

                string lastDateForInvites;
                string tempLastInvite = null; //Temporarily saves the last invite sent

                Band bandInvites = SqliteDataAccess.LoadBand((checkedListBoxBands.SelectedItem as Band).idBand);

                string ID = (checkedListBoxBands.SelectedItem as Band).idBand.ToString();

                LabelUpdate("Sending Invitations");

                //Retrieving Bands Popularity
                Dictionary<string, string> Popularity = await Globals.browserPlaywright.CheckBandPopularity(bandInvites); //City , Popularity

                //Retrieving ticket values
                List<TicketPrice> TicketPrice = SqliteDataAccess.GetTicketsBand(bandInvites);

                if (TicketPrice.Count == 0)
                {
                    MessageBox.Show("Please setup your ticket prices first.");
                    EnableOrDisableButtons(true);
                    return;
                }


                //Retrieving Clubs in the selected Chain
                List<Club> ClubsOfChain = SqliteDataAccess.LoadClubsPerChain(
                    Globals.browserPlaywright.userID,
                    (checkedListBoxChains.SelectedItem as Chain).idChain);

                if (ClubsOfChain.Count == 0)
                {
                    MessageBox.Show("Please add clubs to your chain first.");
                    EnableOrDisableButtons(true);
                    return;
                }

                //Checking the last day available to send invitations
                lastDateForInvites = await Globals.browserPlaywright.GetLastDateForInvites(checkedListBoxBands.SelectedItem as Band);
                if (String.IsNullOrEmpty(lastDateForInvites))
                    return;

                //Retrieving Tour days
                List<TourDay> TourDays = SqliteDataAccess.GetTourDays(
                    Int32.Parse(cmbBoxPPMYear.SelectedItem.ToString()),
                    bandInvites.idBand)
                    .OrderBy(x => x.idTD).ToList();

                //Checks if the band has any concerts in the selected tour Year
                if (TourDays.Count > 0)
                {
                    //Used as a buffer for invite dates to be sent
                    List<TourDay> InviteCalendar = new List<TourDay>();

                    //Checking if Tour dates are higher than last invite and lower than last available date to send invitation
                    foreach (TourDay td in TourDays)
                    {
                        if (!String.IsNullOrEmpty(bandInvites.lastInviteBand))
                        {
                            if (DateTime.ParseExact(td.dateTD, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= DateTime.ParseExact(lastDateForInvites, "dd/MM/yyyy", CultureInfo.InvariantCulture) && DateTime.ParseExact(td.dateTD + " " + td.timeTD, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) > DateTime.ParseExact(bandInvites.lastInviteBand, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture))
                            {
                                InviteCalendar.Add(td);
                            }
                        }
                        else
                        {
                            if (DateTime.ParseExact(td.dateTD, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= DateTime.ParseExact(lastDateForInvites, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                            {
                                InviteCalendar.Add(td);
                            }
                        }
                    }

                    if (InviteCalendar.Count == 0)
                    {
                        MessageBox.Show("There are no new invitations to be sent in this year.");
                        EnableOrDisableButtons(true);
                        return;
                    }

                    //Iterates all the days to send invites
                    foreach (TourDay td in InviteCalendar)
                    {
                        if (!String.IsNullOrEmpty(td.cityTD))
                        {
                            string Venue = null;
                            string Date = null;
                            string Time = null;
                            string TPrice = null;

                            int ArtistCut = -1;
                            int Rider = -1;

                            //Gets Club Id for current TourDay
                            foreach (Club club in ClubsOfChain)
                            {
                                if (club.cityClub.Equals(td.cityTD))
                                {
                                    Venue = club.idClub;
                                    break;
                                }
                            }

                            if (String.IsNullOrEmpty(Venue))
                            {
                                MessageBox.Show($"Error: Club not found in {td.cityTD}");
                                bandInvites.lastInviteBand = tempLastInvite;
                                SqliteDataAccess.UpdateBand(bandInvites);
                                EnableOrDisableButtons(true);
                                return;
                            }


                            Date = td.dateTD;
                            Time = td.timeTD;

                            //Gets ticket price for current TourDay
                            foreach (KeyValuePair<string, string> valuePair in Popularity)
                            {
                                if (valuePair.Key.Equals(td.cityTD))
                                {
                                    foreach (TicketPrice tp in TicketPrice)
                                    {
                                        if (tp.popTicket.Equals(valuePair.Value))
                                        {
                                            TPrice = tp.priceTicket;
                                            break;
                                        }
                                    }
                                }
                            }

                            // Popularity in city not found, defaults to 5$ ticket price.
                            if (String.IsNullOrEmpty(TPrice))
                                TPrice = "5";

                            ArtistCut = (bandInvites).artCutBand;

                            Rider = (bandInvites).riderBand;

                            if (!String.IsNullOrEmpty(Venue) && !String.IsNullOrEmpty(Date) && !String.IsNullOrEmpty(Time) && !String.IsNullOrEmpty(TPrice) && ArtistCut != -1 && Rider != -1)
                            {

                                try {
                                    await Globals.browserPlaywright.SendInvitesVenue(Venue, bandInvites.idBand);
                                    await Globals.browserPlaywright.SendInvitesDate(Date);
                                    await Globals.browserPlaywright.SendInvitesTime(Time);
                                    await Globals.browserPlaywright.SendInvitesTicketPrice(TPrice);
                                    await Globals.browserPlaywright.SendInvitesArtCut(ArtistCut);
                                    await Globals.browserPlaywright.SendInvitesRider(Rider);


                                    switch (await Globals.browserPlaywright.SendInvitesClick())
                                    {
                                        case Status.Success:
                                            tempLastInvite = Date + " " + Time;
                                            break;
                                        case Status.Failed:
                                            //Saves selected band with the last tempLastInvite saved
                                            bandInvites.lastInviteBand = tempLastInvite;
                                            SqliteDataAccess.UpdateBand(bandInvites);
                                            MessageBox.Show("The following last invite couldn't be sent:\n" + td.cityTD + "- " + Date + ".\nPlease check your data and try again.");
                                            EnableOrDisableButtons(true);
                                            return;
                                    }
                                }
                                catch (PlaywrightSharp.PlaywrightSharpException err)
                                {
                                    if (err.Message.Contains("DISCONNECTED"))
                                    {
                                        MessageBox.Show("Error: Check your connection and try again.");
                                        bandInvites.lastInviteBand = tempLastInvite;
                                        SqliteDataAccess.UpdateBand(bandInvites);
                                        EnableOrDisableButtons(true);
                                        return;
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Something went wrong, check all your data.");
                                EnableOrDisableButtons(true);
                                return;
                            }
                        }

                    }

                    //Saves selected band with new LastInvite
                    bandInvites.lastInviteBand = tempLastInvite;
                    SqliteDataAccess.UpdateBand(bandInvites);

                }
                else
                {
                    MessageBox.Show("The selected year doesn't have any concerts.");
                }

                EnableOrDisableButtons(true);
                LabelUpdate();
            }
            catch (PlaywrightSharpException err)
            {
                if (err.Message.Contains("DISCONNECTED"))
                {
                    MessageBox.Show("Error: Check your connection and try again.");
                }
            }

        }        

        public static class ControlInvokeExtensions
        {
            public static void InvokeOnHostThread(Control host, MethodInvoker method)
            {
                if (host.IsHandleCreated)
                    host.Invoke(new EventHandler(delegate { method(); }));
                else
                    method();
            }
        }
    }
}
