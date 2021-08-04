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
    public partial class frmMain : MetroSetForm
    {
        AsyncWebBrowser awb = new AsyncWebBrowser();
        bool loggedIn; // Used to check if User is logged in
        bool serverSet; //Used to confirm the Server the User is in
        bool BoolClosing; //Used to check if program already tried to close (Jumps the message Box)
        bool chainSet; //Used to check if a chain is selected;

        string strServer; //Saves the webserver the user is in for navigation purposes
        string gameLanguage; //Saves Users game language before changing
        string idUtilizador; //Saves the character ID for database interaction
        string currentYear; //Saves the current game year
        string cityTemp; //Temp for city convertion through ID
        Chain selectedChain; //Saves user selected Chain
        Band selectedBand; //Saves user selected Band

        private List<string> listClubs = new List<string>(); //Temp List of Clubs
        private List<Club> userClubs = new List<Club>(); //Saves the new Clubs from a User
        private Dictionary<string, string> charCompanies = new Dictionary<string, string>(); //Saves the company list from the user
        private List<Chain> charChains = new List<Chain>(); //Saves the chain list from the user
        private SortedDictionary<DateTime, string> firstPPMDayMonth = new SortedDictionary<DateTime, string>();
        private SortedDictionary<int, string> validTimeSlots = new SortedDictionary<int, string>
            { { 0, "12:00" }, { 1, "14:00" }, { 2, "16:00" }, { 3, "18:00" }, { 4, "20:00" }, { 5, "22:00" } }; //PPM Time Slots for Shows and respective IDs
        private SortedDictionary<int, string> Scoring = new SortedDictionary<int, string>
            { { 0, "truly abysmal" }, { 1, "abysmal" }, { 2, "bottom dwelling" }, { 3, "horrendous" }, { 4, "dreadful" }, { 5, "terrible" }, 
            { 6, "poor" }, { 7, "below average" }, { 8, "mediocre" }, { 9, "above average" }, { 10, "decent" }, { 11, "nice" }, { 12, "pleasant" },
            { 13, "good" }, { 14, "sweet" }, { 15, "splendid" }, { 16, "awesome" }, { 17, "great" }, { 18, "terrific" }, { 19, "wonderful" },
            { 20, "incredible" }, { 21, "perfect" }, { 22, "revolutionary" }, { 23, "mind melting" }, { 24, "earth shaking" }, { 25, "GOD SMACKING" }, { 26, "GOD SMACKINGLY GLORIOUS" } };
        private SortedDictionary<string, int> validCitySlots = new SortedDictionary<string, int>
        { { "Amsterdam", 8 }, { "Ankara", 35 }, { "Antalya", 61 }, { "Baku", 58 }, { "Barcelona", 9 }, { "Belgrade", 36 },
        { "Berlin", 7 }, { "Brussels", 33 }, { "Bucharest", 46 }, { "Budapest", 42 }, { "Buenos Aires", 17 }, { "Chicago" , 60},
        { "Copenhagen", 22 }, { "Dubrovnik", 29 }, { "Glasgow", 27 }, { "Helsinki", 19 }, { "Istanbul", 60 }, { "Izmir", 22 },
        { "Jakarta", 55 }, { "Johannesburg", 51 }, { "Kiev", 56 }, { "London", 5 }, { "Los Angeles", 14 }, { "Madrid", 24 },
        { "Manila", 54 }, { "Melbourne", 10 }, { "Mexico City", 32 }, { "Milan", 52 }, { "Montreal", 38 }, { "Moscow", 18 },
        { "Nashville", 11 }, { "New York", 6 }, { "Paris", 20 }, { "Porto", 31 }, { "Rio de Janeiro", 25 }, { "Rome", 23 },
        { "São Paulo", 21 }, { "Sarajevo", 49 }, { "Seattle", 50 }, { "Shanghai", 45 }, { "Singapore", 39 }, { "Sofia", 53 },
        { "Stockholm", 1 }, { "Tallin", 34 }, { "Tokyo", 62 }, { "Toronto", 16 }, { "Tromsø", 26 }, { "Warsaw", 48 }, { "Vilnius", 28 }
        }; //PPM Cities and respective IDs
        private SortedDictionary<string, int> validGenreSlots = new SortedDictionary<string, int>{
            {"African Music", 18}, {"Blues", 15}, {"Classical", 16}, {"Country & Western", 13}, {"Electronica", 7}, {"Flamenco", 19},
            {"Heavy Metal", 5}, {"Hip Hop", 9}, {"Jazz", 14}, {"Latin Music", 17}, {"Modern Rock", 4}, {"Pop", 8}, {"Punk Rock", 6},
            {"R&B", 10}, {"Reggae", 11}, {"Rock", 3}, {"World Music", 12}
        }; //PPM Genres and respective IDs

        public frmMain()
        {
            this.InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.frmMain_FormClosing);
            this.Leave += new EventHandler(this.frmMain_Leave);
            this.Load += new EventHandler(this.frmMain_Load);
            checkedListBoxChains.DisplayMember = "Name";
            checkedListBoxBands.DisplayMember = "Name";
        }

        private async void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.BoolClosing)
            {
                if (MessageBox.Show("Would you like to close the client?", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (loggedIn)
                    {
                        if (!ChangeGameLanguageAsync().IsCompleted)
                        {
                            this.BoolClosing = true;
                            this.Hide();
                            e.Cancel = true;
                            await ChangeGameLanguageAsync();
                            Task.WaitAll();
                            this.Close();
                        }
                    }
                }
            }
        }
        private void frmMain_Leave(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.wb.Navigate("https://popmundo.com");

            WbLogin(this.loggedIn);

        }
        private async void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!serverSet)
                GetServer();

            if ((e.Url.ToString().Trim('{').Trim('}').Equals("https://" + strServer + "/World/Popmundo.aspx")) && !loggedIn)
            {
                
                this.awb.SetBrowser(wb, strServer);

                this.loggedIn = true;
                this.serverSet = true;
                WbLogin(this.loggedIn);

                await ChangeGameLanguageAsync();
                
                
                await GetUserIDAsync();
                
                CheckUser(this.idUtilizador);
                
                await GetCurrentYearAsync();

                FillListBoxChains();

            }
            else if ((e.Url.ToString().Trim('{').Trim('}').Equals("https://" + strServer + "/World/Popmundo.aspx/Character")) && !loggedIn) //Checks if the User has logged in and if so hides the WebBrowser class to display the UI.
            {
                this.awb.SetBrowser(wb, strServer);
                this.loggedIn = true;
                this.serverSet = true;
                WbLogin(this.loggedIn);

                if (String.IsNullOrEmpty(gameLanguage))
                    await ChangeGameLanguageAsync();

                await GetUserIDAsync();
                
                CheckUser(this.idUtilizador);

                await GetCurrentYearAsync();

                FillListBoxChains();


            }
        }


        private void GetServer() //Checks if the User has logged in
        {
            string absoluteURI = this.wb.Url.AbsoluteUri;
            string[] getServidor = absoluteURI.Split('/');

            if (getServidor.Length > 2)
            {
                this.strServer = getServidor[2];
            }

        }
        private void WbLogin(bool entry) //Makes the WebBrowser class visible and adjusts the position.
        {
            if (!entry)
            {
                this.wb.Visible = true;
                this.wb.BringToFront();
            }
            else
            {
                this.wb.Visible = false;
                this.wb.SendToBack();
            }
        }
        private DateTime GetInitialDateByYear(int yearPPM)
        {
            DateTime firstPPMDate = new DateTime(2003,01,01);

            int DiferenceYear = yearPPM - 1;

            return (firstPPMDate.AddDays(DiferenceYear * 56));
        }
        private async Task ChangeGameLanguageAsync()
        {
            await awb.NavigateAsync("https://" + strServer + "/User/Popmundo.aspx/User/LanguageSettings");

            LabelUpdate("Updating Language");

            if (String.IsNullOrEmpty(this.gameLanguage))
            {
                foreach (HtmlElement htmlElement in this.wb.Document.GetElementsByTagName("p"))
                {
                    if (htmlElement.GetAttribute("InnerHtml").Contains("ctl00$cphLeftColumn$ctl00$ddlLanguage"))
                    {
                        foreach (HtmlElement child in htmlElement.Children)
                        {
                            if (child.TagName.Equals("SELECT"))
                            {
                                foreach (HtmlElement grandchild in child.Children)
                                {
                                    if (grandchild.GetAttribute("selected").Equals("selected"))
                                    {
                                        this.gameLanguage = grandchild.GetAttribute("value");
                                    }

                                }

                                foreach (HtmlElement grandchild in child.Children)
                                {
                                    grandchild.SetAttribute("selected", "false");

                                    if (grandchild.GetAttribute("value").Equals("24"))
                                    {
                                        grandchild.SetAttribute("selected", "selected");

                                        break;
                                    }

                                }
                            }

                        }
                    }
                }
                foreach (HtmlElement htmlElement in this.wb.Document.GetElementsByTagName("p"))
                {
                    foreach (HtmlElement child in htmlElement.Children)
                    {
                        if (child.GetAttribute("type").Contains("submit"))
                            child.InvokeMember("click");
                    }
                }
            }
            else if ((!this.gameLanguage.Equals("24") && (String.IsNullOrEmpty(this.gameLanguage) == false)))
            {
                foreach (HtmlElement htmlElement in this.wb.Document.GetElementsByTagName("p"))
                {
                    if (htmlElement.GetAttribute("InnerHtml").Contains("ctl00$cphLeftColumn$ctl00$ddlLanguage"))
                    {
                        foreach (HtmlElement child in htmlElement.Children)
                        {
                            if (child.TagName.Equals("SELECT"))
                            {
                                foreach (HtmlElement grandchild in child.Children)
                                {
                                    grandchild.SetAttribute("selected", "false");

                                    if (grandchild.GetAttribute("value").Equals(this.gameLanguage))
                                    {
                                        grandchild.SetAttribute("selected", "selected");

                                        break;
                                    }

                                }
                            }

                        }
                    }
                }
                foreach (HtmlElement htmlElement in this.wb.Document.GetElementsByTagName("p"))
                {
                    foreach (HtmlElement child in htmlElement.Children)
                    {

                        if (child.GetAttribute("type").Contains("submit"))
                            child.InvokeMember("click");
                    }
                }
            }

            LabelUpdate();
        }
        private void LabelUpdate(string status = "Idle")
        {
            this.labelStatus.Text = "Status: " + status;
        }
        private async Task GetUserIDAsync() //Gets the Character ID
        {
            LabelUpdate("Checking User ID");

            if (String.IsNullOrEmpty(this.idUtilizador))
            {
                Task t = awb.NavigateAsync("https://" + strServer + "/World/Popmundo.aspx/Character");
                await t;

                foreach (HtmlElement htmlElement in this.wb.Document.GetElementsByTagName("a"))
                {
                    if (htmlElement.GetAttribute("InnerHtml").Contains("General Information"))
                    {
                        Console.WriteLine("Passei por aqui");
                        this.idUtilizador = htmlElement.GetAttribute("href").ToString().Split('/')[6];
                        break;
                    }
                }
            }
            

            LabelUpdate();
        }
        private void CheckUser(string id)
        {
            LabelUpdate("Checking User");

            if (!SqliteDataAccess.LoadUser(id))
            {
                SqliteDataAccess.SaveUser(id);
            }

            LabelUpdate();
        }
        private List<Club> GetDuplicates(List<Club> input1, List<Club> input2)
        {

            HashSet<Club> hashset = new HashSet<Club>(input1);
            IEnumerable<Club> duplicates = input2.Where(e => !hashset.Add(e));

            List<Club> output = new List<Club>(duplicates);
            return output;
        }
        private async Task GetCurrentYearAsync()
        {
            if (String.IsNullOrEmpty(this.currentYear))
            {
                await awb.NavigateAsync("https://" + this.strServer + "/World/Popmundo.aspx/City/Calendar/1");

                foreach (HtmlElement htmlElement in this.wb.Document.GetElementsByTagName("p"))
                {
                    if (htmlElement.GetAttribute("InnerHtml").Contains("ctl00$cphLeftColumn$ctl00$ddlPPMYear"))
                    {
                        foreach (HtmlElement child in htmlElement.Children)
                        {
                            if (child.TagName.Equals("SELECT"))
                            {
                                foreach (HtmlElement grandchild in child.Children)
                                {
                                    if (grandchild.GetAttribute("selected").Equals("selected"))
                                    {
                                        this.currentYear = grandchild.GetAttribute("value");
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        private List<int> YearList(string year)
        {
            List<int> list = new List<int>();

            if (Int32.TryParse(year, out int intYear))
            {
                for(int i = 0; i < 6; i++)
                {
                    int temp = intYear - 2 + i;
                    list.Add(temp);
                }
            }

            return list;

        }


        private async Task GetLocalesAsync()
        {

            await awb.NavigateAsync("https://" + this.strServer + "/World/Popmundo.aspx/ChooseCompany");

            LabelUpdate("Retrieving Clubs");

            foreach (HtmlElement htmlElement in this.wb.Document.GetElementsByTagName("a"))
            {
                if (htmlElement.GetAttribute("href").Contains("World/Popmundo.aspx/Company/"))
                {
                    string id = htmlElement.GetAttribute("href").ToString().Split('/')[6].Trim('[');
                    string name = htmlElement.InnerText.Trim(']');
                    this.charCompanies.Add(id, name);
                }
            }

            foreach (KeyValuePair<string, string> entry in this.charCompanies)
            {
                await updateLocalesAsync(entry.Key);
            }

            LabelUpdate();
        }
        private async Task<Club> getClubInfoAsync(string club)
        {

            await awb.NavigateAsync("https://" + this.strServer + "/World/Popmundo.aspx/Locale/Management/" + club);

            HtmlElementCollection elementsByTagName = this.wb.Document.GetElementsByTagName("tbody")[0].GetElementsByTagName("tr");
            if (elementsByTagName[0].InnerText.Contains("Club"))
            {
                string clubName = this.wb.Document.GetElementsByTagName("h2")[0].InnerText.Trim(); //Club Name
                //string clubGenre = elementsByTagName[1].GetElementsByTagName("td")[1].InnerText.Split('(')[0].Trim(); // Club Genre (String, NOT ID)
                string[] strArray = elementsByTagName[3].GetElementsByTagName("a")[0].GetAttribute("href").Split('/');
                string cityID = strArray[strArray.Length - 1]; //Club City ID

                if (!String.IsNullOrEmpty(cityID))
                {
                    foreach (var city in validCitySlots)
                    {
                        if (city.Value.Equals(Int32.Parse(cityID)))
                        {
                            this.cityTemp = city.Key;
                        }
                    }
                }


                Club c = new Club
                {
                    Id = club,
                    Name = clubName,
                    City = this.cityTemp,
                    UserId = this.idUtilizador
                };

                return c;

            }


            return null;
        }
        private async Task updateLocalesAsync(String idCompany)
        {
            await awb.NavigateAsync("https://" + this.strServer + "/World/Popmundo.aspx/Company/Locales/" + idCompany);



            foreach (HtmlElement htmlElement in this.wb.Document.GetElementsByTagName("tbody"))
            {
                foreach(HtmlElement htmlElement1 in htmlElement.GetElementsByTagName("tr"))
                {
                    string[] strArray = htmlElement1.GetElementsByTagName("td")[0].GetElementsByTagName("a")[0].GetAttribute("href").Split('/');
                    string idLocale = strArray[strArray.Length - 1];
                    this.listClubs.Add(idLocale);
                }
            }


            foreach (String clubID in listClubs)
            {
                Club tempClub = await getClubInfoAsync(clubID);
                if (tempClub != null)
                {
                    this.userClubs.Add(tempClub);
                }
            }

            this.listClubs.Clear();


        }
        private void GetChainsDB(string id)
        {
            this.charChains = SqliteDataAccess.LoadChains(id);
        }
        private List<Club> CompareClubsDBIn(List<Club> input) //Compares the clubs the user has in-game with the ones in the database
        {
            LabelUpdate("Adding new clubs");
            
            List<Club> dbClubs = new List<Club>(SqliteDataAccess.LoadClubs(this.idUtilizador));

            HashSet<Club> hashset = new HashSet<Club>(dbClubs);
            IEnumerable<Club> nonduplicates = input.Where(e => hashset.Add(e));

            List<Club> output = new List<Club>(nonduplicates);
            
            LabelUpdate();

            return output;          
        }
        private List<Club> CompareClubsDBOut(List<Club> input) //Compares the clubs the user has in the database with the ones in-game 
        {
            LabelUpdate("Deleting missing clubs.");
            List<Club> dbClubs = new List<Club>(SqliteDataAccess.LoadClubs(this.idUtilizador));

            HashSet<Club> hashset = new HashSet<Club>(input);
            IEnumerable<Club> nonduplicates = dbClubs.Where(e => hashset.Add(e));

            List<Club> output = new List<Club>(nonduplicates);

            LabelUpdate();

            return output;
        }
        
        //Get Clubs from the game
        private async void btnRetrieveClubs_Click(object sender, EventArgs e)
        {
            await GetLocalesAsync();
            
            SqliteDataAccess.SaveClubs(CompareClubsDBIn(this.userClubs));
            SqliteDataAccess.DeleteClubs(CompareClubsDBOut(this.userClubs));
            LabelUpdate();
            
        }

        //Chains
        private void checkedListBoxChains_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedChain = checkedListBoxChains.SelectedItem as Chain;
            this.chainSet = true;

            if (checkedListBoxChains.Items.Count > 0)
                FillListBands();
        }
        private void FillListBoxChains()
        {
            GetChainsDB(this.idUtilizador);

            checkedListBoxChains.DataSource = charChains;
        }
        private void btnDeleteChain_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.selectedChain.Id))
            {
                SqliteDataAccess.DeleteChain(this.selectedChain.Id);
                
                FillListBoxChains();
            }
            
        }
        private void btnAddChain_Click(object sender, EventArgs e)
        {
            Form formChain = new frmChain(false, this.idUtilizador);

            formChain.ShowDialog();

            FillListBoxChains();
            
        }
        private void btnEditChain_Click(object sender, EventArgs e)
        {
            if(chainSet)
            {
                Form formChain = new frmChain(true, this.idUtilizador, selectedChain);

                formChain.ShowDialog();

                FillListBoxChains();
            }
        }

        //Bands
        private void btnNewBand_Click(object sender, EventArgs e)
        {
            Form formBand = new frmBand(false, this.idUtilizador, selectedChain);

            formBand.ShowDialog();

            FillListBands();
        }
        private void btnEditBand_Click(object sender, EventArgs e)
        {
            Form formBand = new frmBand(true, this.idUtilizador, selectedChain, selectedBand);

            formBand.ShowDialog();

            FillListBands();
        }
        private void btnDeleteBand_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.DeleteBand(selectedBand.Id);

            FillListBands();
        }
        private void FillListBands()
        {
           checkedListBoxBands.DataSource = SqliteDataAccess.LoadBands(this.selectedChain);
        }
        private void checkedListBoxBands_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedBand = checkedListBoxBands.SelectedItem as Band;

            cmbBoxPPMYear.DataSource = YearList(this.currentYear);
        }
        private async Task<Dictionary<string,string>> CheckBandPopularityAsync()
        {
            LabelUpdate("Checking Popularity levels");

            await awb.NavigateAsync("https://" + strServer + "/World/Popmundo.aspx/Artist/Popularity/" + selectedBand.Id);

            Dictionary<string, string> tempPopularityCity = new Dictionary<string, string>();
            string city = null;
            string popularity = null;

            foreach (HtmlElement htmlElement in wb.Document.GetElementsByTagName("tbody"))
            {
                foreach(HtmlElement htmlElement1 in htmlElement.GetElementsByTagName("tr"))
                {
                    foreach (HtmlElement htmlElement2 in htmlElement1.GetElementsByTagName("td"))
                    {
                        foreach(HtmlElement htmlElement3 in htmlElement2.GetElementsByTagName("a"))
                        {

                            if (htmlElement3.GetAttribute("href").ToString().Split('/')[5].Equals("City"))
                            {
                                city = htmlElement3.InnerText;
                            }
                            else if (htmlElement3.GetAttribute("href").ToString().Split('/')[6].Equals("Scoring"))
                            {
                                popularity = htmlElement3.InnerHtml;
                            }

                            if (!String.IsNullOrEmpty(city) && !String.IsNullOrEmpty(popularity))
                                tempPopularityCity.Add(city, popularity);
                        }
                    }

                    city = null;
                    popularity = null;
                }
            }

                LabelUpdate();

            return tempPopularityCity;
        }

        private void btnTicketPrice_Click(object sender, EventArgs e)
        {
            bool edit;

            edit = SqliteDataAccess.CheckTicketBand(selectedBand);
            List<TicketPrice> tickets = new List<TicketPrice>();

            if (edit)
            {
                tickets = SqliteDataAccess.GetTicketsBand(selectedBand);
                Form formTicket = new frmTicket(edit, selectedBand, idUtilizador, tickets);
                formTicket.ShowDialog();
            }
            else
            {
                Form formTicket = new frmTicket(edit, selectedBand, idUtilizador);
                formTicket.ShowDialog();
            }
            

            
        }
        private void btnTour_Click(object sender, EventArgs e)
        {
            int year = Int32.Parse(cmbBoxPPMYear.SelectedItem.ToString());
            bool edit = SqliteDataAccess.CheckTour(year, Int32.Parse(selectedBand.Id), Int32.Parse(idUtilizador));

            Form formTour = new frmTouring(year, edit, Int32.Parse(idUtilizador), selectedBand);
            formTour.ShowDialog();
        }
        private async void btnSendInvites_ClickAsync(object sender, EventArgs e)
        {
            //List of TourDay

            LabelUpdate("Sending invitations");

            Dictionary<string,string> Popularity = await CheckBandPopularityAsync(); //City , Popularity

            List<TicketPrice> TicketPrice = SqliteDataAccess.GetTicketsBand(selectedBand);

            List<Club> ClubsOfChain = SqliteDataAccess.LoadClubsPerChain(idUtilizador, selectedChain.Id);

            List<TourDay> TourDays = SqliteDataAccess.GetTourDays(Int32.Parse(cmbBoxPPMYear.SelectedItem.ToString()), Int32.Parse(selectedBand.Id)).OrderBy(x => x.Id).ToList();

            if(TourDays.Count > 0)
            {
                
                string lastDateFromInvites = null;
                string tempLastInvite = null;

                await awb.NavigateAsync("https://" + strServer + "/World/Popmundo.aspx/Artist/InviteArtist/" + selectedBand.Id);

                foreach (HtmlElement htmlElement in wb.Document.GetElementsByTagName("tbody"))
                {
                    foreach (HtmlElement htmlElement1 in htmlElement.GetElementsByTagName("tr"))
                    {
                        foreach (HtmlElement htmlElement2 in htmlElement1.GetElementsByTagName("td"))
                        {
                            foreach (HtmlElement child in htmlElement2.Children)
                            {
                                if (child.TagName.Equals("SELECT") && child.Name.Equals("ctl00$cphLeftColumn$ctl01$ddlDay"))
                                {
                                    foreach (HtmlElement grandchild in child.Children)
                                    {
                                        lastDateFromInvites = grandchild.InnerHtml;
                                    }
                                }
                            }
                        }
                    }
                }

                

                List<TourDay> InviteCalendar = new List<TourDay>();

                foreach (TourDay td in TourDays)
                {
                    if (!String.IsNullOrEmpty(selectedBand.LastInvite))
                    {
                        if (DateTime.Parse(td.Day) <= DateTime.Parse(lastDateFromInvites) && DateTime.Parse(td.Day + " " + td.Time) > DateTime.Parse(selectedBand.LastInvite))
                        {
                            InviteCalendar.Add(td);
                        }
                    }
                    else
                    {
                        if (DateTime.Parse(td.Day) <= DateTime.Parse(lastDateFromInvites))
                        {
                            InviteCalendar.Add(td);
                        }
                    }
                }
                foreach (TourDay td in InviteCalendar)
                {
                    if (!String.IsNullOrEmpty(td.City))
                    {
                        //TODO: Verificar se existe clube na cidade em questão, senão saltar e deixar mensagem no final. ("As seguintes cidades não têm clube")
                        //TODO: Possivelmente confirmar clubes antes de envio dos convites.

                        string Venue = null;
                        string Date = null;
                        string Time = null;
                        string TPrice = null;
                        
                        int ArtistCut = -1;
                        int Rider = -1;


                        foreach (Club club in ClubsOfChain) //Gets Club Id for current TourDay
                        {
                            if (club.City.Equals(td.City))
                            {
                                Venue = club.Id;
                                break;
                            }
                        }

                        Date = td.Day;
                        Time = td.Time;

                        foreach (KeyValuePair<string, string> valuePair in Popularity) //Gets ticket price for current TourDay
                        {
                            if (valuePair.Key.Equals(td.City))
                            {
                                foreach (TicketPrice tp in TicketPrice)
                                {
                                    if (tp.Popularity.Equals(valuePair.Value))
                                    {
                                        TPrice = tp.Price;
                                    }
                                }
                            }
                        }
                        if (String.IsNullOrEmpty(TPrice)) // Popularity in city not found, defaults to 5$ ticket price.
                            TPrice = "5";

                        ArtistCut = selectedBand.ArtistCut;

                        Rider = selectedBand.Rider;

                        if (!String.IsNullOrEmpty(Venue) && !String.IsNullOrEmpty(Date) && !String.IsNullOrEmpty(Time) && !String.IsNullOrEmpty(TPrice) && ArtistCut != -1 && Rider != -1)
                        {
                            if (!String.IsNullOrEmpty(td.City))
                            {
                                await awb.SendInviteAsync(Venue, Date, Time, TPrice, ArtistCut, Rider);
                                tempLastInvite = Date + " " + Time;
                            }
                        }
                    }
                }

                selectedBand.LastInvite = tempLastInvite;
                SqliteDataAccess.UpdateBand(selectedBand);

            }
            else
            {
                MessageBox.Show("The selected Tour is empty.");
            }

            LabelUpdate();

        }
    }
}
