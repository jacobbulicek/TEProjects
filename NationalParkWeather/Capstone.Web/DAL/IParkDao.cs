
﻿using Capstone.Web.Models;

﻿using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IParkDao
    {
        IList<Park> GetParks();
        Park GetPark(string parkCode);

        IList<ParkWeather> GetParkWeather(string parkCode);


        List<SelectListItem> GetParkSelectList();

    }
}
