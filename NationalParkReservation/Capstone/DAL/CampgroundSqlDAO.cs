using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO : ICampgroundDAO
    {
        private const string CampgroundSelectQuery = @"SELECT *
                                                FROM campground
                                                INNER JOIN park ON campground.park_id = park.park_id";


        private const string CuyahogaSelectQuery = @"SELECT park_id, name, open_from_mm, open_to_mm, daily_fee
                                                FROM campground
                                                WHERE park_id = 3";

        private const string CampgroundFeeQuery = @"SELECT daily_fee
                                                FROM campground";

        private const string BlackwoodsSelectQuery = @"SELECT campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee
                                                FROM campground
                                                WHERE name = 'Blackwoods'";

        private const string SiteSelectQuery = @"SELECT site_number, max_occupancy, accessible, max_rv_length, utilities, cost
                                             FROM site'";


        private readonly string connectionString;

        // Single Parameter Constructor
        public CampgroundSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }



        /// <summary>
        /// Returns list of campgrounds.
        /// </summary>
        /// 
        public IList<Campground> GetCampgrounds(int parkId)
        {
            List<Campground> campgrounds = new List<Campground>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $@"{CampgroundSelectQuery}
                                  WHERE park_id = {parkId}'";

                query = $@"{CampgroundSelectQuery} WHERE park.park_id = @parkId";


                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@parkId", parkId);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                campgrounds = MapCampgroundQuery(reader);
            }

            return campgrounds;

        }

        //public IList<Campground> Get_Acadia_Campgrounds()
        //{
        //    List <Campground> campgrounds = new List<Campground>();


        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {

        //        SqlCommand command = connection.CreateCommand();
        //        command.CommandText = AcadiaSelectQuery;
        //        connection.Open();

        //        SqlDataReader reader = command.ExecuteReader();

        //        campgrounds = MapCampgroundQuery(reader);
        //    }

        //    return campgrounds;
        //}

        //public IList<Campground> Get_Arches_Campgrounds()
        //{
        //    List<Campground> campgrounds = new List<Campground>();


        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {

        //        SqlCommand command = connection.CreateCommand();
        //        command.CommandText = ArchesSelectQuery;
        //        connection.Open();

        //        SqlDataReader reader = command.ExecuteReader();

        //        campgrounds = MapCampgroundQuery(reader);
        //    }

        //    return campgrounds;
        //}

        //public IList<Campground> Get_Cuyahoga_Campgrounds()
        //{
        //    List<Campground> campgrounds = new List<Campground>();


        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {

        //        SqlCommand command = connection.CreateCommand();
        //        command.CommandText = CuyahogaSelectQuery;
        //        connection.Open();

        //        SqlDataReader reader = command.ExecuteReader();

        //        campgrounds = MapCampgroundQuery(reader);
        //    }

        //    return campgrounds;
        //}


        public decimal GetDailyFee(int inputCampgroundId)
        {
            decimal DailyFee;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = $@"{CampgroundFeeQuery}
                                   WHERE campground_id = '{inputCampgroundId}'";

                query = $@"{CampgroundFeeQuery} WHERE campground_id = @campgroundId";

                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;

                command.Parameters.AddWithValue("@campgroundId", inputCampgroundId);

                connection.Open();

                DailyFee = Convert.ToDecimal(command.ExecuteScalar());

            }


            return DailyFee;

        }




        public IList<Campground> GetBlackwoods()
        {
            List<Campground> campgrounds = new List<Campground>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = connection.CreateCommand();
                command.CommandText = BlackwoodsSelectQuery;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                campgrounds = MapCampgroundQuery(reader);
            }

            return campgrounds;
        }












        /// <summary>
        /// Gives all Park info.
        /// </summary>
        //public IList<Campground> GetParkInfo()
        //{
        //    List<Campground> campgrounds = new List<Campground>();


        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {

        //        SqlCommand command = connection.CreateCommand();
        //        command.CommandText = CampgroundSelectQuery;
        //        connection.Open();

        //        SqlDataReader reader = command.ExecuteReader();

        //        campgrounds = MapCampgroundQuery(reader);
        //    }

        //    return campgrounds;
        //}

        private List<Campground> MapCampgroundQuery(SqlDataReader reader)
        {
            List<Campground> campgrounds = new List<Campground>();

            while (reader.Read())
            {
                Campground campground = new Campground
                {
                    Campground_Id = Convert.ToInt32(reader["campground_id"]),
                    Park_Id = Convert.ToInt32(reader["park_id"]),
                    Name = reader["name"] as string,
                    Open_From_MM = Convert.ToInt32(reader["open_from_mm"]),
                    Open_To_MM = Convert.ToInt32(reader["open_to_mm"]),
                    Daily_Fee = Convert.ToDecimal(reader["daily_fee"])
                };

                campgrounds.Add(campground);
            }
            return campgrounds;
        }
    }
}