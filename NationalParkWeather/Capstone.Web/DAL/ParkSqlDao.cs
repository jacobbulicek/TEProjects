
using Capstone.Web.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ParkSqlDao : IParkDao
    {
        private readonly string connectionString;

        private const string Sql_GetParks = "SELECT * FROM park ORDER BY parkName ASC";


        public ParkSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Park> GetParks()
        {

            List<Park> parks = new List<Park>();
            {

                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql = $"SELECT * FROM park";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        // Create a Park
                        Park park = new Park();
                        park.ParkCode = Convert.ToString(reader["parkCode"]);
                        park.ParkName = Convert.ToString(reader["parkName"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.Acreage = reader["acreage"] as int? ?? default(int);
                        park.ElevationInFeet = reader["elevationInFeet"] as int? ?? default(int);
                        park.MilesOfTrail = Convert.ToDecimal(reader["milesOfTrail"]);
                        park.NumberOfCampsites = reader["numberOfCampsites"] as int? ?? default(int);
                        park.Climate = Convert.ToString(reader["climate"]);
                        park.YearFounded = reader["yearFounded"] as int? ?? default(int);
                        park.AnnualVisitorCount = reader["annualVisitorCount"] as int? ?? default(int);
                        park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        park.EntryFee = reader["entryFee"] as decimal? ?? default(decimal);
                        park.NumberOfAnimalSpecies = reader["numberOfAnimalSpecies"] as int? ?? default(int);


                        parks.Add(MapRowToPark(reader));


                    }
                }
            }
            return parks;
        }


        private Park MapRowToPark(SqlDataReader reader)
        {
            return new Park()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
                State = Convert.ToString(reader["state"]),
                Acreage = reader["acreage"] as int? ?? default(int),
                ElevationInFeet = reader["elevationInFeet"] as int? ?? default(int),
                MilesOfTrail = reader["milesOfTrail"] as int? ?? default(int),
                NumberOfCampsites = reader["numberOfCampsites"] as int? ?? default(int),
                Climate = Convert.ToString(reader["climate"]),
                YearFounded = reader["yearFounded"] as int? ?? default(int),
                AnnualVisitorCount = reader["annualVisitorCount"] as int? ?? default(int),
                InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                ParkDescription = Convert.ToString(reader["parkDescription"]),
                EntryFee = reader["entryFee"] as decimal? ?? default(decimal),
                NumberOfAnimalSpecies = reader["numberOfAnimalSpecies"] as int? ?? default(int)
            };
        }


        public Park GetPark(string parkCode)
        {

            Park result = new Park();
            // Create a new connection object
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the connection
                conn.Open();

                string sql = $"SELECT * FROM park WHERE parkCode = @parkCode";



                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@parkCode", parkCode);
                // Execute the command
                SqlDataReader reader = cmd.ExecuteReader();


                // Loop through each row
                while (reader.Read())
                {
                    // Create a product
                    Park park = new Park();
                    park.ParkCode = Convert.ToString(reader["parkCode"]);
                    park.ParkName = Convert.ToString(reader["parkName"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.Acreage = reader["acreage"] as int? ?? default(int);
                    park.ElevationInFeet = reader["elevationInFeet"] as int? ?? default(int);
                    park.MilesOfTrail = Convert.ToDecimal(reader["milesOfTrail"]);
                    park.NumberOfCampsites = reader["numberOfCampsites"] as int? ?? default(int);
                    park.Climate = Convert.ToString(reader["climate"]);
                    park.YearFounded = reader["yearFounded"] as int? ?? default(int);
                    park.AnnualVisitorCount = reader["annualVisitorCount"] as int? ?? default(int);
                    park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                    park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                    park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                    park.EntryFee = reader["entryFee"] as decimal? ?? default(decimal);
                    park.NumberOfAnimalSpecies = reader["numberOfAnimalSpecies"] as int? ?? default(int);

                    result = park;
                }
                return result;
            }
        }



        public IList<ParkWeather> GetParkWeather(string parkCode)

        {

            List<ParkWeather> weather = new List<ParkWeather>();
            {

                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql = $"SELECT * FROM weather WHERE parkCode = @parkCode";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    // Execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        // Create a Park
                        ParkWeather parkWeather = new ParkWeather();
                        parkWeather.ParkCode = Convert.ToString(reader["parkCode"]);
                        parkWeather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        parkWeather.Low = Convert.ToInt32(reader["low"]);
                        parkWeather.High = Convert.ToInt32(reader["high"]);
                        parkWeather.Forecast = Convert.ToString(reader["forecast"]);
                        parkWeather.WeatherImage = parkWeather.GetWeatherImage();
                        parkWeather.WeatherReccomendation = parkWeather.GetWeatherRec();
                        parkWeather.TempRecommendation = parkWeather.GetTempRec();

                        weather.Add(parkWeather);
                        //weather.Add(MapRowToWeather(reader));
                    }
                }
            }
            return weather;
        }


        //private ParkWeather MapRowToWeather(SqlDataReader reader)
        //{
        //    return new ParkWeather()
        //    {
        //        ParkCode = Convert.ToString(reader["parkCode"]),
        //        FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]),
        //        Low = Convert.ToInt32(reader["low"]),
        //        High = Convert.ToInt32(reader["high"]),
        //        Forecast = Convert.ToString(reader["forecast"]),
        //        WeatherImage = 
        //    };
        //}


        public List<SelectListItem> GetParkSelectList()
        {
            List<SelectListItem> output = new List<SelectListItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = Sql_GetParks;
                    command.Connection = connection;


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();

                        item.Text = Convert.ToString(reader["parkName"]);
                        item.Value = Convert.ToString(reader["parkCode"]);
                        output.Add(item);
                    }
                }
            }
            catch (SqlException ex)
            {
                output = new List<SelectListItem>();
            }

            return output;
        }
    }
}





