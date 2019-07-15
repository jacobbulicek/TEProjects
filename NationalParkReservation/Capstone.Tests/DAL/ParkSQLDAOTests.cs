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
    public class ParkSqlDAOTests : DatabaseTest
    {
        private ParkSqlDAO dao;
        private int newParkId;

        [TestInitialize]
        public override void Setup()
        {
            // Arrange
            base.Setup();
            dao = new ParkSqlDAO(ConnectionString);


            //Add a test park
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    "INSERT INTO park (name, location, establish_date, area, visitors, description) VALUES ('Acadia', 'Ohio', '2010-01-01', '120', '120', 'Park'); SELECT SCOPE_IDENTITY()",
                    connection);

                connection.Open();

                newParkId = Convert.ToInt32(command.ExecuteScalar());
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    "INSERT INTO park (name, location, establish_date, area, visitors, description) VALUES ('Arches', 'Ohio', '2010-01-01', '120', '120', 'Park'); SELECT SCOPE_IDENTITY()",
                    connection);

                connection.Open();

                newParkId = Convert.ToInt32(command.ExecuteScalar());
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    "INSERT INTO park (name, location, establish_date, area, visitors, description) VALUES ('Cuyahoga Valley', 'Ohio', '2010-01-01', '120', '120', 'Park'); SELECT SCOPE_IDENTITY()",
                    connection);

                connection.Open();

                newParkId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        [TestMethod]
        public void Get_Parks_Returns_Park()
        {

            int one;
            string parkName;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                connection.Open();

                using (SqlCommand commandFive = new SqlCommand(@"(SELECT park_id FROM park WHERE name = 'Acadia')", connection))
                {

                    one = (int)commandFive.ExecuteScalar();
                }

                using (SqlCommand commandSix = new SqlCommand(@"(SELECT name FROM park WHERE name = 'Acadia')", connection))
                {

                    parkName = (string)commandSix.ExecuteScalar();
                }

            }

            IList<Park> acadiaPark = dao.GetParks(one);
            // Assert
            Assert.AreEqual(1, acadiaPark.Count);
            Assert.AreEqual(parkName, acadiaPark[0].Name);
            Assert.AreEqual(one, acadiaPark[0].Park_Id);

        }


        [TestMethod]
        public void Get_Acadia_Returns_ParkList()
        {
            // Act
            IList<Park> acadiaPark = dao.GetAcadia();

            // Assert
            Assert.AreEqual(1, acadiaPark.Count);
            Assert.AreEqual("Acadia", acadiaPark[0].Name);
        }

        [TestMethod]
        public void Get_Arches_Returns_ParkList()
        {
            // Act
            IList<Park> archesPark = dao.GetArches();

            // Assert
            Assert.AreEqual(1, archesPark.Count);
            Assert.AreEqual("Arches", archesPark[0].Name);
        }

        [TestMethod]
        public void Get_Cuyahoga_Returns_ParkList()
        {
            // Act
            IList<Park> cuyahogaPark = dao.GetCuyahoga();

            // Assert
            Assert.AreEqual(1, cuyahogaPark.Count);
            Assert.AreEqual("Cuyahoga Valley", cuyahogaPark[0].Name);
        }

        
    }
}





















    











