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
    public class ReservationSqlDAOTests : DatabaseTest
    {
        private ReservationSqlDAO daoR;
        private int newReservationId;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            daoR = new ReservationSqlDAO(ConnectionString);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string reservationSearch = @"
               SET IDENTITY_INSERT Park ON
               INSERT INTO Park (park_id, name, location, establish_date, area, visitors, description)
               VALUES (1, 'Franklin', 'Ohio', '2010-01-01', 120, 120, 'Cool place')
               SET IDENTITY_INSERT PARK OFF
               SET IDENTITY_INSERT campground ON INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee)
               VALUES (144, 1, 'BlackWoods', 1, 12, 35.00)
               SET IDENTITY_INSERT campground OFF
               SET IDENTITY_INSERT site ON INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities)
               VALUES (96, 144, 12, 6, 0, 0, 0)
               SET IDENTITY_INSERT site OFF
               INSERT INTO reservation(site_id, name, from_date, to_date, create_date)
               VALUES((SELECT site_id FROM site WHERE site_number = 12), 'Hobo Family Reservation', '2019-01-01', '2019-12-31', '2010-01-01'); SELECT SCOPE_IDENTITY()";


                SqlCommand command = connection.CreateCommand();
                command.CommandText = reservationSearch;

                connection.Open();

                newReservationId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        [TestMethod]
        public void CreateNew_Reservation_Returns_ReservationID()
        {
            // Assert
            int siteId = 96;
            string reservationName = "Test";
            DateTime StartDate = new DateTime(2019, 1, 1);
            DateTime SeaWallClose = new DateTime(2019, 1, 30);

            int reservationId = daoR.CreateNewReservation(reservationName, StartDate, SeaWallClose, siteId);
            Assert.IsTrue(reservationId > 0);

        }
    }
}

