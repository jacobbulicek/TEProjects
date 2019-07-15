using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationDAO
    {

        private const string CreateReservationQuery = @"INSERT INTO reservation (site_id, name, from_date, to_date, create_date) 
                                                       VALUES (@site_id, @name, @from_date, @to_date, @create_date); 
                                                        SELECT CAST(SCOPE_IDENTITY() as int);";

        private const string ReservationSelectQuery = @"SELECT project_id, name, from_date, to_date
                                                    FROM project";

        

        private readonly string connectionString;

        // Single Parameter Constructor
        public ReservationSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        
        public int CreateNewReservation(string reservationName, DateTime arrivalDate, DateTime departDate, int siteId)
        {
            int reservationId = 0;

            try
            {
                // Creat a SqlConnection to our database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO reservation (site_id, name, from_date, to_date) " +
                                                    "VALUES (@siteId, @reservationName, @arrivalDate, @departDate);", conn);

                    cmd.Parameters.AddWithValue("@siteId", siteId);
                    cmd.Parameters.AddWithValue("@reservationName", reservationName);
                    cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                    cmd.Parameters.AddWithValue("@departDate", departDate);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(reservation_id) FROM reservation;", conn);
                    reservationId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException E)
            {
                Console.WriteLine("An error occurred. " + E.Message);
                throw;
            }

            return reservationId;
        }




       






        //public IList<Reservation> SearchReservations(int campgroundId, DateTime FromDate, DateTime ToDate)
        //{
        //    List<Reservation> reservations = new List<Reservation>();

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        string selectReservation = $@"SELECT s.site_id
        //                                        FROM site s
        //                                        where s.campground_id = @CampgroundId
        //                                        AND s.site_id NOT IN 
        //                                        (
        //                                        SELECT s.site_id from reservation r
        //                                        JOIN site s on r.site_id = s.site_id
        //                                        WHERE s.campground_id = @CampgroundId
        //                                        AND r.to_date > @FromDate AND r.from_date < @ToDate
        //                                        )";







        //        string query = $@"{selectReservation}";

        //        SqlCommand command = connection.CreateCommand();
        //        command.CommandText = query;

        //        command.Parameters.AddWithValue("@CampgroundId", campgroundId);
        //        command.Parameters.AddWithValue("@FromDate", FromDate);
        //        command.Parameters.AddWithValue("@ToDate", ToDate);


        //        connection.Open();

        //        SqlDataReader reader = command.ExecuteReader();

        //        reservations = MapAvailableSitesQuery(reader);




        //    }

        //    return reservations;

        //}


        


       

       




        private List<Reservation> MapAvailableSitesQuery(SqlDataReader reader)
        {
            List<Reservation> reservations = new List<Reservation>();

            while (reader.Read())
            {
                Reservation reservation = new Reservation
                {
                    Reservation_Id = 0,
                    Site_Id = Convert.ToInt32(reader["site_id"]),
                    Name = reader["name"] as string,
                    From_Date = (DateTime)(reader["from_date"]),
                    To_Date = (DateTime)(reader["to_date"]),
                    Create_Date = (DateTime)(reader["create_date"]),
                };

                reservations.Add(reservation);
            }
            return reservations;
        }



        private List<Reservation> MapReservationQuery(SqlDataReader reader)
        {
            List<Reservation> reservations = new List<Reservation>();

            while (reader.Read())
            {
                Reservation reservation = new Reservation
                {
                    Reservation_Id = Convert.ToInt32(reader["reservation_id"]),
                    Site_Id = Convert.ToInt32(reader["site_id"]),
                    Name = reader["name"] as string,
                    From_Date = (DateTime)(reader["from_date"]),
                    To_Date = (DateTime)(reader["to_date"]),
                    Create_Date = (DateTime)(reader["create_date"]),
                };

                reservations.Add(reservation);
            }
            return reservations;
        }
    }
}


