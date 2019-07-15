using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        /// <summary>
        /// The Campground Id
        /// </summary>
        public int Campground_Id { get; set; }

        /// <summary>
        /// The Park Id of the Parent Park
        /// </summary>
        public int Park_Id { get; set; }

        /// <summary>
        /// The Campground Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Open From Month 
        /// </summary>
        public int Open_From_MM { get; set; }

        /// <summary>
        /// The Open To Month 
        /// </summary>
        public int Open_To_MM { get; set; }

        /// <summary>
        /// The Park Date Established
        /// </summary>
        public decimal Daily_Fee { get; set; }

    }
}
