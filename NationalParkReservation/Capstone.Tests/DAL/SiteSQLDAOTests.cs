using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Tests.DAL;
 
namespace Capstone.Tests.DAL
{
    [TestClass]
    public class SiteSqlDAOTests : DatabaseTest
    {
        private SiteSqlDAO daoS;
        private int newSiteId;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            daoS = new SiteSqlDAO(ConnectionString);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string siteSearch = @"
               SET IDENTITY_INSERT Park ON
               INSERT INTO Park (park_id, name, location, establish_date, area, visitors, description)
               VALUES (1, 'Franklin', 'Ohio', '2010-01-01', 120, 120, 'Cool place')
               SET IDENTITY_INSERT PARK OFF
               SET IDENTITY_INSERT campground ON INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee)
               VALUES (144, 1, 'BlackWoods', 1, 12, 35.00)
               SET IDENTITY_INSERT campground OFF
               INSERT INTO site (campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities)
               VALUES ((SELECT campground_id FROM campground WHERE name = 'Blackwoods'), 12, 6, 0, 0, 0); SELECT SCOPE_IDENTITY()";


                SqlCommand command = connection.CreateCommand();
                command.CommandText = siteSearch;

                connection.Open();

                newSiteId = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        
        [TestMethod]
        public void SearchReservation_Returns_Reservation_Searched()
        {
            // Assert
            int campgroundId = 144;
            DateTime StartDate = new DateTime(2019, 1, 1);
            DateTime SeaWallClose = new DateTime(2019, 1, 30);
            
            IList<Site> newReservation = daoS.SearchReservations(campgroundId, StartDate, SeaWallClose);
            int theCount = newReservation.Count;
            Assert.AreEqual(1, theCount);
            Assert.AreNotEqual(2, theCount);
        }
    }
}

//(int campgroundId, DateTime FromDate, DateTime ToDate)