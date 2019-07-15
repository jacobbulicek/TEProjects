using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        /// <summary>
        /// The Reservation Id
        /// </summary>
        public int Reservation_Id { get; set; }

        /// <summary>
        /// The Reservation Id
        /// </summary>
        public int Campground_Id { get; set; }

        /// <summary>
        /// The Site Id
        /// </summary>
        public int Site_Id { get; set; }

        /// <summary>
        /// The Reservation Family Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Starting Date of Reservation
        /// </summary>
        public DateTime From_Date { get; set; }

        /// <summary>
        /// The Date Reservation Ends
        /// </summary>
        public DateTime To_Date { get; set; }

        /// <summary>
        /// The Date the Reservation is Made
        /// </summary>
        public DateTime Create_Date { get; set; }

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

        public int Reservation_Cost { get; set; }

        public int Reservation_Days { get; set; }






















    }
}
