using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteDAO
    {
        private const string SiteSelectQuery = @"SELECT campground_id, name, open_from_mm, open_to_mm, daily_fee
                                                FROM campground";

        private const string SiteAddQuery = "INSERT INTO dbo.department (name) VALUES (@name)";

        private const string SitesSelectQuery = @"SELECT *
                                             FROM site";

        public IList<Site> GetAllSites()
        {
            List<Site> sites = new List<Site>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                SqlCommand command = connection.CreateCommand();
                command.CommandText = SitesSelectQuery;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                sites = MapSiteQuery(reader);
            }
            return sites;
        }


        private readonly string connectionString;

        public SiteSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }




        public IList<Site> SearchReservations(int campgroundId, DateTime FromDate, DateTime ToDate)
        {
            List<Site> availableSites = new List<Site>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectReservation = $@"SELECT TOP 5 *
                                                FROM site s
                                                where s.campground_id = @CampgroundId
                                                AND s.site_id NOT IN 
                                                (
                                                SELECT s.site_id from reservation r
                                                JOIN site s on r.site_id = s.site_id
                                                WHERE s.campground_id = @CampgroundId
                                                AND r.to_date > @FromDate AND r.from_date < @ToDate
                                                )";



                string query = $@"{selectReservation}";

                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;

                command.Parameters.AddWithValue("@CampgroundId", campgroundId);
                command.Parameters.AddWithValue("@FromDate", FromDate);
                command.Parameters.AddWithValue("@ToDate", ToDate);


                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                availableSites = MapSiteQuery(reader);




            }

            return availableSites;

        }








        private List<Site> MapSiteQuery(SqlDataReader reader)
        {
            List<Site> sites = new List<Site>();

            while (reader.Read())
            {
                Site site = new Site
                {
                    Site_Id = Convert.ToInt32(reader["site_id"]),
                    Campground_Id = Convert.ToInt32(reader["campground_id"]),
                    Site_Number = Convert.ToInt32(reader["site_number"]),
                    Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]),
                    Accessible = (bool)(reader["accessible"]),
                    Max_Rv_Length = Convert.ToInt32(reader["max_rv_length"]),
                    Utilities = (bool)(reader["utilities"]),
                };

                sites.Add(site);
            }
            return sites;
        }
    }
}
