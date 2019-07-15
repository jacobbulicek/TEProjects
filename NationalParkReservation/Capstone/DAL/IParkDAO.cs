using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IParkDAO
    {
        /// <summary>
        /// Returns a list of all parks.
        /// </summary>
        /// <returns></returns>
        IList<Park> GetAcadia();

        /// <summary>
        /// Gives all Park info.
        /// </summary>
        IList<Park> GetArches();

        IList<Park> GetCuyahoga();

        IList<Park> GetParks(int parkId);
    }
}