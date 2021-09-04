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
        public static bool LoadUser(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new { ID = id };
                var sql = "Select * from tbl_User Where idUser = @ID";
                var result = cnn.Query(sql, parameters);
                if (result.Count() > 0)
                    return true;

                return false;
            }
        }
        public static void SaveUser(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new { ID = id };
                var sql = "Insert into tbl_User (idUser) values(@ID)";
                cnn.Execute(sql, parameters);
            }
        }

        //Clubs
        public static List<Club> LoadClubs(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new { ID = id };
                var sql = "Select * from tbl_Club Where idUserClub = @ID";
                var result = cnn.Query<Club>(sql, parameters);

                
                return result.ToList();
            }
        }
        public static List<Club> LoadClubsPerChain(int idUser, string idChain = null)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql;
                var parameters = new { IDUser = idUser, IDChain = idChain };
                if (!String.IsNullOrEmpty(parameters.IDChain))
                    sql = "Select * from tbl_Club Where idChainClub = @IDChain And idUserClub = @IDUser";
                else
                    sql = "Select * from tbl_Club Where idChainClub Is null And idUserClub = @IDUser";

                var result = cnn.Query<Club>(sql, parameters).ToList();

                return result;
            }
        }
        public static void SaveClubs(List<Club> clubs)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (Club club in clubs) 
                {
                    var parameteres = new
                    {
                        ID = club.idClub,
                        Name = club.nameClub,
                        City = club.cityClub,
                        IDChain = club.idChainClub,
                        IDUser = club.idUserClub
                    };

                    var sql = "Insert into tbl_Club (idClub, nameClub, cityClub, idChainClub, idUserClub) values (@ID, @Name, @City, @IDChain, @IDUser)";
                                    
                    cnn.Execute(sql, parameteres);
                }
            }
        }
        public static void DeleteClubs(List<Club> clubs)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (Club club in clubs)
                {
                    var parameters = new { ID = club.idClub };
                    var sql = "Delete from tbl_Club Where idClub = @ID";

                    cnn.Execute(sql, parameters);
                }
            }
        }
        public static void UpdateChainInClubs(List<Club> clubs)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (Club club in clubs) 
                {
                    var parameters = new { ID = club.idClub, IDChain = club.idChainClub};
                    var sql = "Update tbl_Club Set idChainClub = @IDChain Where idClub = @ID";
                
                    cnn.Execute(sql, parameters);
                }
            }
        }
        
        //Chains
        public static List<Chain> LoadChains(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new { ID = id};
                var sql = "Select * from tbl_Chain Where idUserChain = @ID";
                var result = cnn.Query<Chain>(sql, parameters).ToList();
                
                return result;
            }
        }
        public static Chain LoadChainByName(int id, string name)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new { ID = id, Name = name };
                var sql = "Select * from tbl_Chain Where idUserChain = @ID And nameChain = @Name";
                var result = cnn.QuerySingle<Chain>(sql, parameters);

                return result;
            }
        }
        public static void SaveChain(Chain chain)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Insert into tbl_Chain (nameChain, idUserChain) values (@nameChain, @idUserChain)", chain);
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
                cnn.Execute("Update tbl_Chain Set nameChain=@chainName, Where idChain = @chainId", new { chainName = chain.nameChain, chainId = chain.idChain });
            }
        }

        //Bands
        public static void SaveBand(Band band)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Insert into tbl_Band (idBand, nameBand, artCutBand, idChainBand, idUserBand, riderBand) values (@idBand, @nameBand, @artCutBand, @idChainBand, @idUserBand, @riderBand)", band);
            }
        }
        public static void UpdateBand(Band band)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Update tbl_Band Set nameBand=@nameBand, riderBand=@riderBand, artCutBand=@artCutBand, lastInviteBand=@lastInviteBand Where idBand = @idBand", new { band.nameBand, band.riderBand, band.artCutBand, band.lastInviteBand, band.idBand });
            }
        }
        public static Band LoadBand(int idBand)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new { Id = idBand };
                var sql = "Select * from tbl_Band Where idBand = @ID";
                var result = cnn.QuerySingle<Band>(sql, parameters);

                return result;
            }
        }
        public static List<Band> LoadBands(Chain chain)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new {Id = chain.idChain };
                var sql = "Select * from tbl_Band Where idChainBand = @ID";
                var result = cnn.Query<Band>(sql, parameters).ToList();

                return result;
            }
        }
        public static void DeleteBand(int id)
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
                var output = cnn.Query("Select * from tbl_Ticket Where idBandTicket = @idBandTicket", new { idBandTicket = band.idBand }).ToList();

                if (output.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }
        public static List<TicketPrice> GetTicketsBand(Band band)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TicketPrice>("Select * from tbl_Ticket Where idBandTicket = @idBandTicket", new { idBandTicket = band.idBand }).ToList();

                return output;
            }
        }
        public static void SaveTickets(List<TicketPrice> tickets)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (TicketPrice ticket in tickets)
                {
                    cnn.Execute("Insert into tbl_Ticket (popTicket, priceTicket, idUserTicket, idBandTicket) values (@popTicket, @priceTicket, @idUserTicket, @idBandTicket)", ticket);
                }
            }
        }
        public static void UpdateTickets(List<TicketPrice> tickets)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (TicketPrice ticket in tickets)
                {
                    cnn.Execute("Update tbl_Ticket Set popTicket=@popTicket, priceTicket=@priceTicket Where idTicket = @idTicket", new { ticket.popTicket, ticket.priceTicket, ticket.idTicket });
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
        public static Tour GetTour(int year, int idBand, int idUser) 
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.QuerySingle<Tour>("Select * from tbl_Tour Where yearTour=@Year AND idBandTour=@idBand AND idUserTour=@idUser", new { year, idBand, idUser });
                
                return output;
            }
            
        }
        public static void UpdateTour(Tour tour)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                
                    cnn.Execute("Update tbl_Tour Set yearTour=@yearTour, typeTour=@typeTour Where idTour = @idTour", new { tour.yearTour, tour.typeTour, tour.idTour});
                
            }
        }
        public static bool CheckTourDay(TourDay day)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new { Day = day.dateTD, Time = day.timeTD, City = day.cityTD };
                var sql = "Select * from tbl_TourDay Where dateTD = @Day AND timeTD = @Time AND cityTD = @City";
                var result = cnn.Query(sql, parameters);

                if (result.Count() > 0)
                    return true;

                return false;
            }
        }
        public static List<TourDay> GetTourDays(int year, int idBand)
        {
            

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new {Year =  year, IDBand = idBand };

                var sql = "Select * from tbl_TourDay " +
                    "INNER JOIN tbl_Tour ON tbl_TourDay.idTourTD = tbl_Tour.idTour " +
                    "WHERE tbl_Tour.yearTour = @Year AND tbl_Tour.idBandTour = @IDBand ";

                var result = cnn.Query<TourDay>(sql, parameters).ToList();

                return result;
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
                        cnn.Execute("Insert into tbl_TourDay (dateTD, timeTD, cityTD, textBoxNameTD, idUserTD, idTourTD) values (@dateTD, @timeTD, @cityTD, @textBoxNameTD , @idUserTD, @idTourTD)", tourday);
                    }

            }
        }
       

    }
}
