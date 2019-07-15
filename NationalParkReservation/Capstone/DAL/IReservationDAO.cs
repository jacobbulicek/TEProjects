using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IReservationDAO
    {


      

        int CreateNewReservation(string reservationName, DateTime arrivalDate, DateTime departDate, int siteId);



    }
}