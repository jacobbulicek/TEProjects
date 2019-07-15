using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkDAO
    {

        private const string AcadiaSelectQuery = @"SELECT park_id, name, location, establish_date, area, visitors, description
                                                FROM Park
                                                WHERE name = 'Acadia'";

        private const string ArchesSelectQuery = @"SELECT park_id, name, location, establish_date, area, visitors, description
                                                FROM Park
                                                WHERE name = 'Arches'";

        private const string CuyahogaSelectQuery = @"SELECT park_id, name, location, establish_date, area, visitors, description
                                                FROM Park
                                                WHERE name = 'Cuyahoga Valley'";

        private const string ParkSelectQuery = @"SELECT park_id, name, location, establish_date, area, visitors, description
                                                FROM Park";

        private const string ParkAddQuery = "INSERT INTO dbo.park (name) VALUES (@name)";

        private readonly string connectionString;

        // Single Parameter Constructor
        public ParkSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Park> GetParks(int parkId)
        {
            List<Park> parks = new List<Park>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $@"{ParkSelectQuery}
                                  WHERE park_id = {parkId}'";

                query = $@"{ParkSelectQuery} WHERE park_id = @parkId";


                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@parkId", parkId);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                parks = MapParkQuery(reader);
            }

            return parks;

        }

        public IList<Park> GetAcadia()
        {
            List<Park> parks = new List<Park>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = connection.CreateCommand();
                command.CommandText = AcadiaSelectQuery;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                parks = MapParkQuery(reader);
            }

            return parks;
        }

        public IList<Park> GetArches()
        {
            List<Park> parks = new List<Park>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = connection.CreateCommand();
                command.CommandText = ArchesSelectQuery;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                parks = MapParkQuery(reader);
            }

            return parks;
        }

        public IList<Park> GetCuyahoga()
        {
            List<Park> parks = new List<Park>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = connection.CreateCommand();
                command.CommandText = CuyahogaSelectQuery;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                parks = MapParkQuery(reader);
            }

            return parks;
        }

        /// <summary>
        /// Gives all Park info.
        /// </summary>


        private List<Park> MapParkQuery(SqlDataReader reader)
        {
            List<Park> parks = new List<Park>();

            while (reader.Read())
            {
                Park park = new Park
                {
                    Park_Id = Convert.ToInt32(reader["park_id"]),
                    Name = reader["name"] as string,
                    Location = reader["location"] as string,
                    Establish_Date = (DateTime)(reader["establish_date"]),
                    Area = Convert.ToInt32(reader["area"]),
                    Visitors = Convert.ToInt32(reader["visitors"]),
                    Description = reader["description"] as string
                };


                parks.Add(park);
            }
            return parks;
        }
    }
}