using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {

        /// <summary>
        /// The Park Id
        /// </summary>
        public int Park_Id { get; set; }

        /// <summary>
        /// The Park Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Park Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The Park Date Established
        /// </summary>
        public DateTime Establish_Date { get; set; }


        /// <summary>
        /// The Park Area in Acres
        /// </summary>
        public int Area { get; set; }

        /// <summary>
        /// The Park Annual Visitors
        /// </summary>
        public int Visitors { get; set; }

        /// <summary>
        /// The Park Description
        /// </summary>
        public string Description { get; set; }

    }
}
