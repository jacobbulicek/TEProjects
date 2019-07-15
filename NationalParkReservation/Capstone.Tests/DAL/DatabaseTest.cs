using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

namespace Capstone.Tests.DAL
{
    [TestClass]
    public abstract class DatabaseTest
    {
        private IConfigurationRoot config;

        /// <summary>
        /// The transaction for each test.
        /// </summary>
        private TransactionScope transaction;

        /// <summary>
        /// The Configuration options specified in appsettings.json
        /// </summary>
        protected IConfigurationRoot Config
        {
            get
            {
                if (config == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

                    config = builder.Build();
                }
                return config;
            }
        }

        /// <summary>
        /// The database connection string derived from the configuration settings
        /// </summary>
        protected string ConnectionString
        {
            get
            {
                return Config.GetConnectionString("npcampground");
            }
        }

        [TestInitialize]
        public virtual void Setup()
        {
            // Begin the transaction
            transaction = new TransactionScope();

            //Get the SQL Script to run
            string sql = @"DELETE FROM reservation;
                            DELETE FROM site;
                            DELETE FROM campground;
                            DELETE FROM park;";

            
            string sqlParkInsert = @"INSERT INTO park(name, location, establish_date, area, visitors, description) VALUES ('Ye Olde Test Site', 'Alaska', '1990-03-22', '15000', '2000000', 'Its a park!'); SELECT SCOPE_IDENTITY()";
            string sqlCampgroundInsert = @"INSERT INTO campground(park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES ((SELECT park_id FROM park WHERE park.name = 'Ye Olde Test Site'), 'Ancient Burial Ground', 1, 12, 69.00)";
            string sqlSiteInsert = @"INSERT INTO site(campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES ((SELECT campground_id FROM campground WHERE campground.name = 'Ancient Burial Ground'), 15, 12, 1, 0, 0)";
            string sqlReservationInsert = @"INSERT INTO reservation(site_id, name, from_date, to_date, create_date) VALUES ((SELECT site_id FROM site WHERE site_number = 15), 'Test Family Reservation', '2019-01-01', '2019-12-01', '2018-12-31')";


            // Execute the script
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlParkInsert, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCampgroundInsert, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlSiteInsert, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlReservationInsert, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        /// <summary>
        /// Gets the row count for a table.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
