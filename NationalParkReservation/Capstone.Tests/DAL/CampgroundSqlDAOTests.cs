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
    public class CampgroundSqlDAOTests : DatabaseTest
    {
        private CampgroundSqlDAO daoC;
        private int newCampgroundId;

        [TestInitialize]
        public override void Setup()
        {
            // Arrange
            base.Setup();
            daoC = new CampgroundSqlDAO(ConnectionString);

            // Add a test campground
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string campSearch = @"
                SET IDENTITY_INSERT Park ON
                INSERT INTO Park (park_id, name, location, establish_date, area, visitors, description)
                VALUES (1, 'Franklin', 'Ohio', '2010-01-01', 120, 120, 'Cool place')
                SET IDENTITY_INSERT PARK OFF
                INSERT INTO campground (park_id, name, open_from_mm, open_to_mm, daily_fee) 
                VALUES (1, 'BlackWoods', 1, 12, 35.00); SELECT SCOPE_IDENTITY()";

                SqlCommand command = connection.CreateCommand();
                command.CommandText = campSearch;

                connection.Open();

                newCampgroundId = Convert.ToInt32(command.ExecuteScalar());
            }
        }


        [TestMethod]
        public void Get_Blackwoods_Returns_Blackwoods()
        {
            // Act
            IList<Campground> blackwoodsCampground = daoC.GetBlackwoods();

            // Assert
            Assert.AreEqual(1, blackwoodsCampground.Count);
            Assert.AreEqual(newCampgroundId, blackwoodsCampground[0].Campground_Id);
            Assert.AreEqual("BlackWoods", blackwoodsCampground[0].Name);
        }

        [TestMethod]
        public void GetDailyFee()
        {
            // Act
            Campground newCampground = new Campground
            {
                Campground_Id = 1,
                Park_Id = 1,
                Name = "New Campground",
                Open_From_MM = 10,
                Open_To_MM = 10,
                Daily_Fee = 35.00M,
            };

            // Assert

            decimal actualResult = daoC.GetDailyFee(newCampgroundId);

            Assert.AreEqual(newCampground.Daily_Fee, actualResult);
        }



        [TestMethod]
        public void Get_Campgrounds()
        {

            int one;
            string campgroundName;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                connection.Open();

                using (SqlCommand commandFive = new SqlCommand(@"(SELECT park_id FROM park WHERE name = 'Franklin')", connection))
                {

                    one = (int)commandFive.ExecuteScalar();
                }

                using (SqlCommand commandSix = new SqlCommand(@"(SELECT name FROM campground WHERE name = 'Blackwoods')", connection))
                {

                    campgroundName = (string)commandSix.ExecuteScalar();
                }

            }

            IList<Campground> blackwoodsCamp = daoC.GetCampgrounds(one);
            // Assert
            Assert.AreEqual(1, blackwoodsCamp.Count);
            Assert.AreEqual(campgroundName, blackwoodsCamp[0].Name);
            Assert.AreEqual(one, blackwoodsCamp[0].Park_Id);

        }





























    }
}

