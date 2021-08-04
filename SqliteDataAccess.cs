using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissingC
{
    class SqliteDataAccess
    {
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        //User
        public static bool LoadUser(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_User Where idUser=@id", new { Id = id });
                if (output.Count() > 0)
                {
                    return true;
                }

                return false;
            }
        }
        public static void SaveUser(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Insert into tbl_User (idUser) values(@id)", new { id });
            }
        }

        //Clubs
        public static List<Club> LoadClubs(string id)
        {
            List<Club> outputClubs = new List<Club>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_Club Where idUserClub=@id", new { Id = id }).ToList();

                foreach (IDictionary<string, object> row in output)
                {
                    Club c = new Club();

                    foreach (var pair in row)
                    {


                        if (pair.Key.Equals("idClub"))
                        {
                            c.Id = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("nameClub"))
                        {
                            c.Name = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("cityClub"))
                        {
                            c.City = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("idChainClub"))
                        {
                            if (pair.Value == null) 
                            {
                                c.ChainId = null;
                            }
                            else
                            {
                                c.ChainId = pair.Value.ToString();
                            }
                        }
                        else if (pair.Key.Equals("idUserClub"))
                        {
                            c.UserId = pair.Value.ToString();
                        };


                    }

                    outputClubs.Add(c);
                }

                return outputClubs;
            }
        }
        public static List<Club> LoadClubsPerChain(string idUser, string chain = null)
        {
            List<Club> outputClubs = new List<Club>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                if (!String.IsNullOrEmpty(chain))
                {
                    var output = cnn.Query("Select * from tbl_Club Where idUserClub=@Id And idChainClub = @idChain", new { Id = idUser, idChain = chain });

                    foreach (IDictionary<string, object> row in output)
                    {
                        Club c = new Club();

                        foreach (var pair in row)
                        {


                            if (pair.Key.Equals("idClub"))
                            {
                                c.Id = pair.Value.ToString();
                            }
                            else if (pair.Key.Equals("nameClub"))
                            {
                                c.Name = pair.Value.ToString();
                            }
                            else if (pair.Key.Equals("cityClub"))
                            {
                                c.City = pair.Value.ToString();
                            }
                            else if (pair.Key.Equals("idChainClub"))
                            {
                                if (pair.Value == null)
                                    c.ChainId = null;
                                else
                                    c.ChainId = pair.Value.ToString();
                            }
                            else if (pair.Key.Equals("idUserClub"))
                            {
                                c.UserId = pair.Value.ToString();
                            };


                        }

                        outputClubs.Add(c);
                    }

                }
                else
                {
                    var output = cnn.Query("Select * from tbl_Club Where idChainClub Is null And idUserClub=@id", new { Id = idUser });

                    foreach (IDictionary<string, object> row in output)
                    {
                        Club c = new Club();

                        foreach (var pair in row)
                        {


                            if (pair.Key.Equals("idClub"))
                            {
                                c.Id = pair.Value.ToString();
                            }
                            else if (pair.Key.Equals("nameClub"))
                            {
                                c.Name = pair.Value.ToString();
                            }
                            else if (pair.Key.Equals("cityClub"))
                            {
                                c.City = pair.Value.ToString();
                            }
                            else if (pair.Key.Equals("idChainClub"))
                            {
                                if (pair.Value == null)
                                    c.ChainId = null;
                            }
                            else if (pair.Key.Equals("idUserClub"))
                            {
                                c.UserId = pair.Value.ToString();
                            };


                        }

                        outputClubs.Add(c);
                    }
                }    
                
                return outputClubs;
            }
        }
        public static void SaveClubs(List<Club> clubs)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (Club club in clubs)
                    cnn.Execute("Insert into tbl_Club (idClub, nameClub, cityClub, idChainClub, idUserClub) values (@Id, @Name, @City, @ChainId, @UserId)", club);
            }
        }
        public static void DeleteClubs(List<Club> clubs)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (Club club in clubs)
                    cnn.Execute("Delete from tbl_Club Where idClub=@Id", new { club.Id });
            }
        }
        public static void UpdateChaininClubs(List<Club> clubs)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (Club club in clubs)
                    cnn.Execute("Update tbl_Club Set idChainClub = @idChain Where idClub = @id", new { idChain = club.ChainId, id = club.Id });
            }
        }
        
        //Chains
        public static List<Chain> LoadChains(string id)
        {
            List<Chain> outputChain = new List<Chain>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_Chain Where idUserChain=@id", new { Id = id }).ToList();

                foreach (IDictionary<string, object> row in output)
                {
                    Chain c = new Chain();

                    foreach (var pair in row)
                    {


                        if (pair.Key.Equals("idChain"))
                        {
                            c.Id = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("nameChain"))
                        {
                            c.Name = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("genreChain"))
                        {
                            c.Genre = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("idUserChain"))
                        {
                            c.UserId = pair.Value.ToString();
                        };


                    }

                    outputChain.Add(c);
                }

                return outputChain;
            }
        }
        public static Chain LoadChainByName(string id, string name)
        {
            Chain c = new Chain();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_Chain Where idUserChain=@id And nameChain=@name", new { Id = id, Name = name }).ToList();

                foreach (IDictionary<string, object> row in output)
                {
                    foreach (var pair in row)
                    {
                        if (pair.Key.Equals("idChain"))
                        {
                            c.Id = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("nameChain"))
                        {
                            c.Name = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("genreChain"))
                        {
                            c.Genre = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("idUserChain"))
                        {
                            c.UserId = pair.Value.ToString();
                        };
                    }
                }

                return c;
            }
        }
        public static void SaveChain(Chain chain)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Insert into tbl_Chain (nameChain, genreChain, idUserChain) values (@Name, @Genre, @UserId)", chain);
            }
        }
        public static void DeleteChain(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(@"Delete from tbl_Chain Where idChain=@Id", new { Id = id });
            }
        }
        public static void UpdateChain(Chain chain)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Update tbl_Chain Set nameChain=@chainName, genreChain=@chainGenre Where idChain = @chainId", new { chainName = chain.Name, chainGenre = chain.Genre, chainId = chain.Id });
            }
        }

        //Bands
        public static void SaveBand(Band band)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Insert into tbl_Band (idBand, nameBand, artCutBand, idChainBand, idUserBand, riderBand) values (@Id, @Name, @ArtistCut, @ChainId, @UserId, @Rider)", band);
            }
        }
        public static void UpdateBand(Band band)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Update tbl_Band Set nameBand=@Name, riderBand=@Rider, artCutBand=@ArtistCut, lastInviteBand=@LastInvite Where idBand = @Id", new { band.Name, band.Rider, band.ArtistCut, band.LastInvite, band.Id });
            }
        }
        
        public static List<Band> LoadBands(Chain chain)
        {
            List<Band> outputBand = new List<Band>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_Band Where idChainBand=@id", new { chain.Id }).ToList();

                foreach (IDictionary<string, object> row in output)
                {
                    Band b = new Band();

                    foreach (var pair in row)
                    {


                        if (pair.Key.Equals("idBand"))
                        {
                            b.Id = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("nameBand"))
                        {
                            b.Name = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("artCutBand"))
                        {
                            b.ArtistCut = Int32.Parse(pair.Value.ToString());
                        }
                        else if (pair.Key.Equals("riderBand"))
                        {
                            b.Rider = Int32.Parse(pair.Value.ToString());
                        }
                        else if (pair.Key.Equals("idChainBand"))
                        {
                            b.ChainId = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("idUserBand"))
                        {
                            b.UserId = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("lastInviteBand"))
                        {
                            if (pair.Value == null)
                            {
                                b.LastInvite = null;
                            }
                            else
                            {
                                b.LastInvite = pair.Value.ToString();
                            }
                        };


                    }

                    outputBand.Add(b);
                }

                return outputBand;
            }
        }
        public static void DeleteBand(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(@"Delete from tbl_Band Where idBand=@Id", new { Id = id });
            }
        }

        //Tickets
        public static bool CheckTicketBand(Band band)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_Ticket Where idBandTicket=@id", new { band.Id }).ToList();

                if (output.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }
        public static List<TicketPrice> GetTicketsBand(Band band)
        {
            List<TicketPrice> outputTicket = new List<TicketPrice>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_Ticket Where idBandTicket=@id", new { band.Id }).ToList();

                foreach (IDictionary<string, object> row in output)
                {
                    TicketPrice t = new TicketPrice();

                    foreach (var pair in row)
                    {


                        if (pair.Key.Equals("idTicket"))
                        {
                            t.Id = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("popTicket"))
                        {
                            t.Popularity = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("priceTicket"))
                        {
                            t.Price = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("idUserTicket"))
                        {
                            t.UserId = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("idBandTicket"))
                        {
                            t.BandId = pair.Value.ToString();
                        };


                    }

                    outputTicket.Add(t);
                }

                return outputTicket;


            }
        }
        public static void SaveTickets(List<TicketPrice> tickets)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (TicketPrice ticket in tickets)
                {
                    cnn.Execute("Insert into tbl_Ticket (popTicket, priceTicket, idUserTicket, idBandTicket) values (@Popularity, @Price, @UserId, @BandId)", ticket);
                }
            }
        }
        public static void UpdateTickets(List<TicketPrice> tickets)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (TicketPrice ticket in tickets)
                {
                    cnn.Execute("Update tbl_Ticket Set popTicket=@Popularity, priceTicket=@Price Where idTicket = @Id", new { ticket.Popularity, ticket.Price, ticket.Id });
                }
            }
        }

        //Tours
        public static void SaveTour(int year, string type, int idBand, int idUser)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Insert into tbl_Tour (yearTour, typeTour, idBandTour, idUserTour) values (@year, @type, @idBand, @idUser)", new { year, type, idBand, idUser });
            }
        }
        public static bool CheckTour(int year, int idBand, int idUser)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_Tour Where yearTour=@Year AND idBandTour=@idBand AND idUserTour=@idUser", new { year, idBand, idUser }).ToList();

                if (output.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }
        public static Tour GetTour(int year, int idBand, int idUser) {

            Tour tour = new Tour();
            

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_Tour Where yearTour=@Year AND idBandTour=@idBand AND idUserTour=@idUser", new { year, idBand, idUser }).ToList();

                foreach (IDictionary<string, object> row in output)
                {
                    foreach (var pair in row)
                    {
                        if (pair.Key.Equals("idTour"))
                        {
                            tour.Id = Int32.Parse(pair.Value.ToString());
                        }
                        else if (pair.Key.Equals("yearTour"))
                        {
                            tour.Year = Int32.Parse(pair.Value.ToString());
                        }
                        else if (pair.Key.Equals("typeTour"))
                        {
                            tour.Type = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("idBandTour"))
                        {
                            tour.idBand = Int32.Parse(pair.Value.ToString());
                        }
                        else if (pair.Key.Equals("idUserTour"))
                        {
                            tour.idUser = Int32.Parse(pair.Value.ToString());
                        }
                    }
                }
            }

            return tour;
        }
        public static void UpdateTour(Tour tour)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                
                    cnn.Execute("Update tbl_Tour Set yearTour=@Year, typeTour=@Type Where idTour = @Id", new { tour.Year, tour.Type, tour.Id});
                
            }
        }
        public static bool CheckTourDay(TourDay day)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_TourDay Where dateTD=@Day AND timeTD=@Time AND cityTD=@City", new { day.Day, day.Time, day.City }).ToList();

                if (output.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }
        public static List<TourDay> GetTourDays(int year, int idBand)
        {
            List<TourDay> outputTour = new List<TourDay>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("Select * from tbl_TourDay INNER JOIN tbl_Tour ON tbl_TourDay.idTourTD=tbl_Tour.idTour WHERE tbl_Tour.yearTour=@Year AND tbl_Tour.idBandTour=@idBand ", new {year, idBand }).ToList();

                foreach (IDictionary<string, object> row in output)
                {
                    TourDay td = new TourDay();

                    foreach (var pair in row)
                    {


                        if (pair.Key.Equals("idTD"))
                        {
                            td.Id = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("dateTD"))
                        {
                            td.Day = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("timeTD"))
                        {
                            td.Time = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("cityTD"))
                        {
                            td.City = pair.Value.ToString();
                        }
                        else if (pair.Key.Equals("idUserTD"))
                        {
                            td.UserId = Int32.Parse(pair.Value.ToString());
                        }
                        else if (pair.Key.Equals("idTourTD"))
                        {
                            td.TourId = Int32.Parse(pair.Value.ToString());
                        };


                    }

                    outputTour.Add(td);
                }

                return outputTour;


            }
        }
        public static void DeleteTourDays(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM tbl_TourDay WHERE idTourTD=@Id", new { Id = id });
            }

        }
        public static void SaveTourDays(List<TourDay> tourdays)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                    foreach (TourDay tourday in tourdays)
                    {
                        cnn.Execute("Insert into tbl_TourDay (dateTD, timeTD, cityTD, idUserTD, idTourTD) values (@Day, @Time, @City, @UserId, @TourId)", tourday);
                    }

            }
        }
       

    }
}
