﻿using Microsoft.Extensions.Logging;
using PlaywrightSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissingC
{
    public enum Status
    {
        Success,
        Failed,
        WrongURL
    }

    public class BrowserPlayWright
    {
        IBrowser browser;
        IPage page;
        string initialPage;
        public string userLanguage;
        public int userID = -1;
        public string serverURL;
        public string gameYear;

        public BrowserPlayWright(string initialPage)
        {
            this.initialPage = initialPage;
        }

        public async Task Init()
        {
            try
            {
                var playwright = await Playwright.CreateAsync();
                this.browser = await playwright.Chromium.LaunchAsync(headless: true);
                this.page = await browser.NewPageAsync();


                await this.page.GoToAsync(this.initialPage);
                Console.WriteLine(page.Url);
            }
            catch(PlaywrightSharpException e)
            {
                if (e.Message.Contains("DISCONNECTED"))
                {
                    MessageBox.Show("Error: Check your connection and try again."); 
                    System.Environment.Exit(1);                  
                }
            }
        }

        public async Task<Status> Login(string username, string password)
        {
            if (page.Url.StartsWith(this.initialPage))
            {
                Regex regexpage = new Regex(@"^https:\/\/\d+\.popmundo\.com.*", RegexOptions.Compiled);
                await page.FillAsync("#ctl00_cphRightColumn_ucLogin_txtUsername", username);
                await page.FillAsync("#ctl00_cphRightColumn_ucLogin_txtPassword", password);
                await page.ClickAsync("#ctl00_cphRightColumn_ucLogin_btnLogin");


                var taskWaitingLoginSuccess = page.WaitForSelectorAsync("#ctl00_ctl08_btnLogout");
                var taskWaitingForErrorNotification = page.WaitForSelectorAsync(".notification-error");



                Task taskCompleted = await Task.WhenAny(new Task[] { taskWaitingLoginSuccess, taskWaitingForErrorNotification });

                if (taskCompleted == taskWaitingLoginSuccess)
                {
                    return Status.Success;
                }
                else if (taskCompleted == taskWaitingForErrorNotification)
                {
                    return Status.Failed;
                }
            }

            return Status.WrongURL;

        }

        public async Task GetUserID()
        {
            try
            {
                await page.GoToAsync("https://" + serverURL + "/World/Popmundo.aspx/Character/");
            }
            catch (PlaywrightSharpException e)
            {
                if (e.Message.Contains("DISCONNECTED"))
                {
                    MessageBox.Show("Error: Check your connection and try again.");
                }
            }

            var idSelector = await page.QuerySelectorAsync("#ppm-sidemenu > .box > .menu > ul > li > a");

            var id = await idSelector.GetAttributeAsync("href");
            id = id.Split('/')[id.Split('/').Length - 1];

            this.userID = Int32.Parse(id);
        }

        public async Task GoTo(string url)
        {
            try
            {
                await page.GoToAsync(url);
            }
            catch (PlaywrightSharpException e)
            {
                if (e.Message.Contains("DISCONNECTED"))
                {
                    MessageBox.Show("Error: Check your connection and try again.");
                }
            }
        }

        public async Task GetServer()
        {
            await page.WaitForLoadStateAsync();
            this.serverURL = page.Url.Split('/')[2];
        }

        public bool CheckURLContains(string contains)
        {
            if (page.Url.Contains(contains))
                return true;

            return false;
        }
        public bool CheckEqualityURL(string url)
        {
            if (page.Url.Equals(url))
                return true;

            return false;
        }

        public async Task<List<Character>> GetCharacters()
        {
            Regex regexpage = new Regex(@"^background: url\('([^']*)'\) no-repeat;$", RegexOptions.Compiled);

            List<Character> characters = new List<Character>();
            await page.WaitForLoadStateAsync();
            var characterBoxs = await page.QuerySelectorAllAsync(".box.clear");
            foreach (var characterBox in characterBoxs)
            {
                Character c = new Character();
                var tempName = await characterBox.QuerySelectorAsync("h2 > a ");
                var Name = await tempName.GetInnerTextAsync();

                if (!String.IsNullOrEmpty(Name))
                {
                    c.Name = Name;
                }

                var tempPortrait = await characterBox.QuerySelectorAsync(".avatar.bmargin10.pointer.idTrigger");
                var Portrait = await tempPortrait.GetAttributeAsync("style");
                Portrait = regexpage.Match(Portrait).Groups[0].ToString();

                if (!String.IsNullOrEmpty(Portrait))
                {
                    c.Portrait = Portrait;
                }

                var tempID = await characterBox.QuerySelectorAsync(".idHolder");
                var ID = await tempID.GetInnerTextAsync();

                if (!String.IsNullOrEmpty(ID))
                {
                    c.ID = Int32.Parse(ID);
                }

                characters.Add(c);
            }

            return characters;
        }

        public async Task ChooseCharacter(string id)
        {
            this.userID = Int32.Parse(id);
            await page.WaitForLoadStateAsync();
            var characterBoxs = await page.QuerySelectorAllAsync(".box.clear");
            foreach (var characterBox in characterBoxs)
            {
                var tempCharacter = await characterBox.QuerySelectorAsync(".idHolder");
                var chosenCharacter = await tempCharacter.GetInnerTextAsync();
                if (chosenCharacter.Equals(id))
                {
                    var buttonInput = await characterBox.QuerySelectorAsync("input");
                    await buttonInput.ClickAsync();

                    await page.WaitForLoadStateAsync();
                    break;
                }
            }
        }

        public async Task GetCurrentGameLanguage()
        {
            await page.GoToAsync("https://" + serverURL + "/User/Popmundo.aspx/User/LanguageSettings");

            var selectedLanguage = await page.QuerySelectorAsync("#ctl00_cphLeftColumn_ctl00_ddlLanguage > option[selected='selected']");

            this.userLanguage = await selectedLanguage.GetAttributeAsync("value");
        }

        public async Task ChangeGameLanguage(string id = "24")
        {
            if (!page.Url.Equals("https://" + serverURL + "/User/Popmundo.aspx/User/LanguageSettings"))
                await page.GoToAsync("https://" + serverURL + "/User/Popmundo.aspx/User/LanguageSettings");

            var selectLanguage = await page.QuerySelectorAsync($"#ctl00_cphLeftColumn_ctl00_ddlLanguage");
            Console.WriteLine("Selected the language" + id);
            await selectLanguage.SelectOptionAsync(id);

            await page.ClickAsync("#ctl00_cphLeftColumn_ctl00_btnSetLocale");
            Console.WriteLine("Clicked");

        }

        public async Task GetCurrentGameYear()
        {
            await page.GoToAsync("https://" + serverURL + "/World/Popmundo.aspx/City/Calendar/1");

            var getIngameYear = await page.QuerySelectorAsync("#ctl00_cphLeftColumn_ctl00_ddlPPMYear > option[selected='selected']");
            this.gameYear = await getIngameYear.GetAttributeAsync("value");
        }

        public async Task<Dictionary<string, string>> GetUserCompanies()
        {
            await page.GoToAsync("https://" + this.serverURL + "/World/Popmundo.aspx/ChooseCompany");

            Dictionary<string, string> CompaniesDict = new Dictionary<string, string>();

            var companies = await page.QuerySelectorAllAsync("#tablecompanies > tbody > tr");

            foreach (var company in companies)
            {
                var td = await company.QuerySelectorAsync("td > a");

                string id = await td.GetAttributeAsync("href");
                id = id.Split('/')[4].Trim('[');
                string name = await td.GetInnerTextAsync();

                CompaniesDict.Add(id, name);
            }
            return CompaniesDict;
        }

        public async Task<List<string>> GetUserLocalesID(Dictionary<string, string> companies)
        {
            List<string> LocalID = new List<string>();
            foreach (KeyValuePair<string, string> company in companies)
            {
                await page.GoToAsync("https://" + serverURL + "/World/Popmundo.aspx/Company/Locales/" + company.Key);

                var tableLocales = await page.QuerySelectorAllAsync("#tablelocales > tbody > tr");

                foreach (var local in tableLocales)
                {
                    var td = await local.QuerySelectorAsync("td > a");

                    string id = await td.GetAttributeAsync("href");
                    id = id.Split('/')[id.Split('/').Length - 1];
                    LocalID.Add(id);
                }
            }
            return LocalID;
        }

        public async Task<List<Club>> GetUserClubs(List<string> LocalIDs)
        {
            List<Club> ClubList = new List<Club>();

            foreach (var id in LocalIDs)
            {
                await page.GoToAsync("https://" + serverURL + "/World/Popmundo.aspx/Locale/Management/" + id);
                var localDetailsTemp = await page.QuerySelectorAllAsync("#ctl00_cphLeftColumn_ctl00_divBasicInfo > table.width100 > tbody > tr");
                var localDetails = localDetailsTemp.ToArray();

                var clubType = await localDetails[0].GetInnerTextAsync();

                if (clubType.Contains("Club"))
                {
                    var nameTemp = await page.QuerySelectorAsync("#ctl00_cphLeftColumn_ctl01_divLocaleInfo > .localebox > h2");
                    var name = await nameTemp.GetInnerTextAsync();
                    name = name.Trim();

                    var cityTemp = await localDetails[3].QuerySelectorAsync("#ctl00_cphLeftColumn_ctl00_lnkLocaleCity");
                    var city = await cityTemp.GetInnerTextAsync();

                    Club c = new Club
                    {
                        idClub = id,
                        nameClub = name,
                        cityClub = city,
                        idUserClub = this.userID
                    };

                    ClubList.Add(c);
                }
            }

            return ClubList;
        }

        public async Task<Dictionary<string, string>> CheckBandPopularity(Band band)
        {
            await page.GoToAsync("https://" + serverURL + "/World/Popmundo.aspx/Artist/Popularity/" + band.idBand);

            Dictionary<string, string> PopularityCity = new Dictionary<string, string>();

            var tableFame = await page.QuerySelectorAllAsync("#tablefame > tbody > tr");

            foreach (var fame in tableFame)
            {
                string city = null;
                string popularity = null;
                var tds = await fame.QuerySelectorAllAsync("td > a");

                foreach (var td in tds)
                {
                    var tempCity = await td.GetAttributeAsync("href");
                    if (tempCity.Contains("/World/Popmundo.aspx/City/"))
                    {
                        city = await td.GetInnerTextAsync();
                    }

                    var tempPopularity = await td.GetAttributeAsync("href");
                    if (tempPopularity.Contains("/World/Popmundo.aspx/Help/Scoring/"))
                    {
                        popularity = await td.GetInnerTextAsync();
                    }
                }


                if (!String.IsNullOrEmpty(city) && !String.IsNullOrEmpty(popularity))
                    PopularityCity.Add(city, popularity);
            }

            return PopularityCity;
        }

        public async Task<string> GetLastDateForInvites(Band band)
        {
            string returnLastDate;
            List<string> dates = new List<string>();

            await page.GoToAsync("https://" + serverURL + "/World/Popmundo.aspx/Artist/InviteArtist/" + band.idBand);

            var optionDates = await page.QuerySelectorAllAsync("#ctl00_cphLeftColumn_ctl01_ddlDay > option");

            foreach (var option in optionDates)
            {
                var optionInnerText = await option.GetInnerTextAsync();
                dates.Add(optionInnerText);
            }

            returnLastDate = dates.ToArray()[dates.Count - 1];

            return returnLastDate;
        }

        public async Task SendInvitesVenue(string Venue, int idBand)
        {
            if (!page.Url.Equals("https://" + Globals.browserPlaywright.serverURL + "/World/Popmundo.aspx/Artist/InviteArtist/" + idBand))
                await page.GoToAsync("https://" + Globals.browserPlaywright.serverURL + "/World/Popmundo.aspx/Artist/InviteArtist/" + idBand);

            var venueSelector = await page.QuerySelectorAsync("#ctl00_cphLeftColumn_ctl01_ddlVenues");

            Task.WaitAll(new Task[] { venueSelector.SelectOptionAsync(Venue), page.WaitForNavigationAsync() });


        }
        public async Task SendInvitesDate(string Date)
        {
            var options = await page.QuerySelectorAllAsync("#ctl00_cphLeftColumn_ctl01_ddlDay > option");

            foreach (var option in options)
            {
                var InnerText = await option.GetInnerTextAsync();
                if (InnerText.Contains(Date))
                {
                    var value = await option.GetAttributeAsync("value");
                    var dateSelector = await page.QuerySelectorAsync("#ctl00_cphLeftColumn_ctl01_ddlDay");
                    await dateSelector.SelectOptionAsync(value);
                    break;
                }
            }
        }
        public async Task SendInvitesTime(string Time)
        {
            var timeSelect = Helper.validTimeSlots.SingleOrDefault(c => c.Value == Time);

            var timeSelector = await page.QuerySelectorAsync("#ctl00_cphLeftColumn_ctl01_ddlHours");
            await timeSelector.SelectOptionAsync(timeSelect.Key.ToString());
        }
        public async Task SendInvitesTicketPrice(string TPrice)
        {
            var priceSelector = await page.QuerySelectorAsync("#ctl00_cphLeftColumn_ctl01_txtTicketPrice");
            await priceSelector.FillAsync(TPrice);
        }
        public async Task SendInvitesArtCut(int ArtistCut)
        {
            var artCutSelector = await page.QuerySelectorAsync("#ctl00_cphLeftColumn_ctl01_txtArtistCut");
            await artCutSelector.FillAsync(ArtistCut + "%");
        }
        public async Task SendInvitesRider(int Rider)
        {
            var riderSelector = await page.QuerySelectorAsync("#ctl00_cphLeftColumn_ctl01_txtRider");
            await riderSelector.FillAsync(Rider.ToString());
        }
        public async Task<Status> SendInvitesClick()
        {
            var buttonInvite = await page.QuerySelectorAsync("#ctl00_cphLeftColumn_ctl01_btnInvite");
            await buttonInvite.ClickAsync();

            await page.WaitForLoadStateAsync();

            var taskWaitingForErrorNotification = page.WaitForSelectorAsync(".notification-error");
            var taskWaitingForSuccessNotification = page.WaitForSelectorAsync(".notification-success");

            if (!browser.IsConnected)
                return Status.Failed;

            Task taskCompleted = await Task.WhenAny(new Task[] { taskWaitingForSuccessNotification, taskWaitingForErrorNotification });

            if (taskCompleted == taskWaitingForSuccessNotification)
            {
                return Status.Success;
            }
            else
            {
                return Status.Failed;
            }
        }
    }
}
