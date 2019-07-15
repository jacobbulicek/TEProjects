using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Site
    {
        /// <summary>
        /// The Site Id
        /// </summary>
        public int Site_Id { get; set; }

        /// <summary>
        /// The Campground Id
        /// </summary>
        public int Campground_Id { get; set; }

        /// <summary>
        /// The Site Number
        /// </summary>
        public int Site_Number { get; set; }

        /// <summary>
        /// The Max Occupancy .. Default 6, can be 12.
        /// </summary>
        public int Max_Occupancy { get; set; }


        /// <summary>
        /// Accessible
        /// </summary>
        public bool Accessible { get; set; }

        /// <summary>
        /// The Max_Rv_Length
        /// </summary>
        public int Max_Rv_Length { get; set; }


        /// <summary>
        /// Utilities
        /// </summary>
        public bool Utilities { get; set; }

        public DateTime From_Date { get; set; }

        public DateTime To_Date { get; set; }

    }
}
