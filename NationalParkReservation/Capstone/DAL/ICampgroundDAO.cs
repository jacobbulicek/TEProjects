using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface ICampgroundDAO
    {

        /// Acadia


        IList<Campground> GetBlackwoods();

        IList<Campground> GetCampgrounds(int parkId);
        /// Arches





        /// Cuyahoga




        decimal GetDailyFee(int inputCampgroundId);

        /// Gives all Park info.

        //IList<Campground> GetParkInfo();


    }
}