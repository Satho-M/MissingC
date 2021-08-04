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
    public class AsyncWebBrowser
    {
        protected WebBrowser m_WebBrowser;
        string strServer;

        private ManualResetEvent m_MRE = new ManualResetEvent(false);

        public void SetBrowser(WebBrowser browser, string strServer)
        {
            this.m_WebBrowser = browser;
            this.strServer = strServer;

            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
        }

        public Task NavigateAsync(string url)
        {
            Navigate(url);

            return Task.Factory.StartNew((Action)(() => {
                m_MRE.WaitOne();
                m_MRE.Reset();
            }));
        }


        public void Navigate(string url)
        {
            m_WebBrowser.Navigate(url);
        }

        public Task SendInviteAsync(string Venue, string Date, string Time, string TPrice, int ArtistCut, int Rider)
        {
           SendInvite(Venue, Date, Time, TPrice, ArtistCut, Rider);

            return Task.Factory.StartNew((Action)(() => {
                m_MRE.WaitOne();
                m_MRE.Reset();
            }));
        }

        public void SendInvite(string Venue, string Date, string Time, string TPrice, int ArtistCut, int Rider)
        {

            foreach (HtmlElement htmlElement in m_WebBrowser.Document.GetElementsByTagName("tbody"))
            {
                foreach (HtmlElement htmlElement1 in htmlElement.GetElementsByTagName("tr"))
                {
                    foreach (HtmlElement htmlElement2 in htmlElement1.GetElementsByTagName("td"))
                    {
                        foreach (HtmlElement child in htmlElement2.Children)
                        {
                            if (child.TagName.Equals("SELECT") && child.Name.Equals("ctl00$cphLeftColumn$ctl01$ddlVenues")) //Selects the Club
                            {
                                foreach (HtmlElement grandchild in child.Children)
                                {
                                    grandchild.SetAttribute("selected", "false");

                                    if (grandchild.GetAttribute("value").Equals(Venue))
                                    {
                                        grandchild.SetAttribute("selected", "selected");

                                        break;
                                    }
                                }
                            }
                            else if (child.TagName.Equals("SELECT") && child.Name.Equals("ctl00$cphLeftColumn$ctl01$ddlDay")) //Selects the Date
                            {
                                foreach (HtmlElement grandchild in child.Children)
                                {
                                    grandchild.SetAttribute("selected", "false");

                                    if (grandchild.InnerText.Equals(Date))
                                    {
                                        grandchild.SetAttribute("selected", "selected");

                                        break;
                                    }

                                }
                            }
                            else if (child.TagName.Equals("SELECT") && child.Name.Equals("ctl00$cphLeftColumn$ctl01$ddlHours")) //Selects the Time
                            {
                                foreach (HtmlElement grandchild in child.Children)
                                {
                                    grandchild.SetAttribute("selected", "false");

                                    if (grandchild.InnerText.Equals(Time))
                                    {
                                        grandchild.SetAttribute("selected", "selected");

                                        break;
                                    }

                                }
                            }
                            else if (child.Name.Equals("ctl00$cphLeftColumn$ctl01$txtTicketPrice")) //Fills in Price
                            {
                                child.SetAttribute("value", TPrice);
                            }
                            else if (child.Name.Equals("ctl00$cphLeftColumn$ctl01$txtArtistCut")) //Fills in ArtistCut
                            {
                                child.SetAttribute("value", ArtistCut + "%");
                            }
                            else if (child.Name.Equals("ctl00$cphLeftColumn$ctl01$txtRider")) //Fills in Rider
                            {
                                child.SetAttribute("value", Rider.ToString());
                            }
                        }
                    }
                }
            }
            foreach (HtmlElement htmlElement in m_WebBrowser.Document.GetElementsByTagName("p"))
            {
                foreach (HtmlElement child in htmlElement.Children)
                {

                    if (child.GetAttribute("type").Contains("submit"))
                        child.InvokeMember("click");
                }
            }

        }

        void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (Regex.IsMatch(e.Url.ToString(), @"^https:\/\/\d+\.popmundo\.com.*"))
                m_MRE.Set();
        }
    }
}
