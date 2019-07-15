using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface ISiteDAO
    {



        IList<Site> SearchReservations(int campgroundId, DateTime FromDate, DateTime ToDate);

        //IList<Site> SearchSitesForReservation(Site newSites);

        IList<Site> GetAllSites();



    }
}
