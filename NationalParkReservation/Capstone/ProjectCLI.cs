using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class ProjectCLI
    {
        const string Command_Acadia = "1";
        const string Command_Arches = "2";
        const string Command_CuyahogaNatlValley = "3";
        const string Command_Quit = "Q";
        const string Command_QuitLower = "q";


        readonly Dictionary<int, string> Dates = new Dictionary<int, string>()
        {

            { 1, "January"}, { 2, "February"}, { 3, "March"}, { 4, "April"}, { 5, "May"}, { 6, "June"}, { 7, "July"},
            { 8, "August"}, { 9, "September"}, { 10, "October"}, { 11, "November"}, { 12, "December"}

        };

        readonly Dictionary<Boolean, string> Utilities = new Dictionary<Boolean, string>()
        {

            { false, "N/A"}, { true, "Yes"}
        };



        private IParkDAO parkDAO;
        private ICampgroundDAO campgroundDAO;
        private IReservationDAO reservationDAO;
        private ISiteDAO siteDAO;

        public ProjectCLI(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO)
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.siteDAO = siteDAO;
            this.reservationDAO = reservationDAO;
        }

        Park newPark = new Park();


        public void RunCLI()
        {
            Welcome();
            PrintMenu();

            string command = Console.ReadLine();


            bool method = true;
            while (method == true)
            {
                switch (command)
                {
                    case Command_Acadia:
                        Console.Clear();
                        newPark.Park_Id = 1;
                        GetParks(newPark.Park_Id);
                        break;

                    case Command_Arches:
                        Console.Clear();
                        newPark.Park_Id = 2;
                        GetParks(newPark.Park_Id);
                        break;

                    case Command_CuyahogaNatlValley:
                        Console.Clear();
                        newPark.Park_Id = 3;
                        GetParks(newPark.Park_Id);
                        break;

                    case Command_Quit:
                        Console.Clear();
                        Console.WriteLine("Thank you for using the national parks reservation system");
                        return;

                    case Command_QuitLower:
                        Console.Clear();
                        Console.WriteLine("Thank you for using the national parks reservation system");
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("The command provided was not a valid command.");
                        Console.WriteLine();
                        Console.WriteLine("Better luck next time.");
                        Console.ReadLine();
                        method = false;
                        break;

                }

                PrintMenu();
            }
        }


        public bool CampgroundMenu()
        {

            Console.WriteLine();
            Console.WriteLine("Select a command");
            Console.WriteLine(" 1 - View Campgrounds");
            Console.WriteLine(" 2 - Search for Reservation");
            Console.WriteLine(" 3 - Return to Previous Screen");
            Console.WriteLine(" Q - Quit");



            Console.WriteLine();


            bool validSecondInput = false;

            string command = Console.ReadLine();

            while (!validSecondInput)
            {
                switch (command)
                {
                    case "1":
                        Console.Clear();
                        GetCampgrounds();
                        validSecondInput = true;
                        break;

                    case "2":
                        Console.Clear();
                        GetCampgrounds();
                        SearchReservations();
                        validSecondInput = true;
                        break;

                    case "3":
                        Console.Clear();
                        PrintMenu();
                        validSecondInput = true;
                        break;

                    case Command_Quit:
                        Console.Clear();
                        validSecondInput = true;
                        return true;

                    case Command_QuitLower:
                        Console.Clear();
                        validSecondInput = true;
                        return true;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;

                }
            }
            return true;

        }

        public void ReservationMenu()
        {

            Console.WriteLine();
            Console.WriteLine("Select a command");
            Console.WriteLine(" 1 - Search for Available Reservation");
            Console.WriteLine(" 2 - Return to Main Menu");
            Console.WriteLine(" Q - Quit");



            Console.WriteLine();


            bool validSecondInput = false;

            string command = Console.ReadLine();

            while (!validSecondInput)


            {
                switch (command)
                {
                    case "1":

                        SearchReservations();
                        validSecondInput = true;
                        break;

                    case "2":
                        Console.Clear();
                        PrintMenu();
                        validSecondInput = true;
                        break;

                    case "4":
                        Console.WriteLine("Thank you for using the national parks reservation system");
                        validSecondInput = true;
                        break;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;

                }
            }




        }


        private void GetAllSites()
        {
            IList<Site> sites = siteDAO.GetAllSites();

            if (sites.Count > 0)
            {
                foreach (Site site in sites)
                {
                    Console.WriteLine("Park Name: " + site.Site_Number.ToString().PadRight(20));
                    Console.WriteLine("Location: " + site.Max_Occupancy.ToString().PadRight(15));
                    Console.WriteLine("Established: " + site.Accessible.ToString().PadRight(12));
                    Console.WriteLine("Area: " + site.Max_Rv_Length.ToString().PadRight(8));
                    Console.WriteLine("Visitors: " + site.Utilities.ToString().PadRight(10));
                    Console.WriteLine();

                    Console.WriteLine();

                }
                CampgroundMenu();
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
                Console.ReadLine();
            }

        }







        private void GetParks(int parkId)
        {
            IList<Park> parks = parkDAO.GetParks(newPark.Park_Id);

            if (parks.Count > 0)
            {
                foreach (Park park in parks)
                {
                    Console.WriteLine("Park Name: " + park.Name.ToString().PadRight(20));
                    Console.WriteLine("Location: " + park.Location.PadRight(15));
                    Console.WriteLine("Established: " + park.Establish_Date.ToString().PadRight(12));
                    Console.WriteLine("Area: " + park.Area.ToString().PadRight(8));
                    Console.WriteLine("Visitors: " + park.Visitors.ToString().PadRight(10));
                    Console.WriteLine();
                    Console.WriteLine("Description: " + park.Description.ToString().PadRight(10));
                    Console.WriteLine();

                }
                CampgroundMenu();
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
                Console.ReadLine();
            }

        }


        List<int> campgroundId = new List<int>();
        List<int> siteIdList = new List<int>();


        private void GetCampgrounds()
        {
            IList<Campground> campgrounds = campgroundDAO.GetCampgrounds(newPark.Park_Id);
            Console.Write("Number: ".PadRight(10));
            Console.Write("Name: ".PadRight(33));
            Console.Write("Open: ".PadRight(10));
            Console.Write("Close: ".PadRight(10));
            Console.Write("Daily Fee: ".PadRight(20));
            Console.WriteLine();




            if (campgrounds.Count > 0)
            {

                foreach (Campground campground in campgrounds)
                {
                    campgroundId.Add(campground.Campground_Id);
                    newCampground.Daily_Fee = Convert.ToDecimal(campground.Daily_Fee);
                    Console.WriteLine(campground.Campground_Id.ToString().PadRight(10) + campground.Name.PadRight(33) + Dates[campground.Open_From_MM].PadRight(10) + Dates[campground.Open_To_MM].PadRight(10) + "$" + newCampground.Daily_Fee.ToString("0.00").PadRight(20));
                    Console.WriteLine();

                }

                ReservationMenu();
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("**** NO CAMPGROUNDS FOUND ****");
            }
        }

        Reservation newReservation = new Reservation();

        Campground newCampground = new Campground();

        private void SearchReservations()
        {
            int campgroundIdNumber = CLIHelper.GetInteger("Please Select a Campground: ");


            newCampground.Campground_Id = campgroundIdNumber;
            DateTime StartDate = new DateTime(2019, 5, 1);
            DateTime SeaWallClose = new DateTime(2019, 9, 30);
            DateTime SchoodicClose = new DateTime(2019, 10, 31);
            DateTime UapcClose = new DateTime(2019, 11, 30);




            if (campgroundId.Contains(newCampground.Campground_Id) == false)
            {
                Console.WriteLine();
                Console.WriteLine("Please select a valid campground.");
                Console.WriteLine();
                SearchReservations();
            }

            DateTime FromDate = CLIHelper.GetDateTime("What is the arrival date? YYYY-MM-DD");

            if (newCampground.Campground_Id == 2 || newCampground.Campground_Id == 3 || newCampground.Campground_Id == 7)
            {
                if (FromDate < StartDate && FromDate != StartDate)
                {
                    Console.WriteLine();
                    Console.WriteLine("The campground is closed.");
                    Console.WriteLine();
                    SearchReservations();
                }
            }

            DateTime ToDate = CLIHelper.GetDateTime("What is the depature date? YYYY-MM-DD");
            if (newCampground.Campground_Id == 2)
            {
                if (ToDate > SeaWallClose && ToDate != SeaWallClose)
                {
                    Console.WriteLine();
                    Console.WriteLine("The campground closes on September 30th.");
                    Console.WriteLine();
                    SearchReservations();
                }
            }
            if (newCampground.Campground_Id == 3)
            {
                if (ToDate > SchoodicClose && ToDate != SchoodicClose)
                {
                    Console.WriteLine();
                    Console.WriteLine("The campground closes on October 31st.");
                    Console.WriteLine();
                    SearchReservations();
                }
            }
            if (newCampground.Campground_Id == 7)
            {
                if (ToDate > SchoodicClose && ToDate != SchoodicClose)
                {
                    Console.WriteLine();
                    Console.WriteLine("The campground closes on November 30th.");
                    Console.WriteLine();
                    SearchReservations();
                }
            }



            Console.Clear();


            newCampground.Daily_Fee = Convert.ToDecimal(campgroundDAO.GetDailyFee(newCampground.Campground_Id));
            newReservation.From_Date = FromDate;
            newReservation.To_Date = ToDate;
            int numberOfDays = (ToDate - FromDate).Days;


            IList<Site> siteReservations = siteDAO.SearchReservations(newCampground.Campground_Id, FromDate, ToDate);





            if (siteReservations.Count > 0)
            {
                Console.WriteLine("Results Matching Your Search Criteria");
                Console.WriteLine();
                Console.WriteLine("SiteID".PadRight(11) + "Max Occupancy".PadRight(15) + "Is Accesible".PadRight(15) + "Max RV Length".PadRight(15) + "Utilities".PadRight(14) + "Total Cost".PadRight(15));
                foreach (Site detail in siteReservations)
                {
                    string accessible;

                    siteIdList.Add(detail.Site_Id);

                    if (detail.Accessible == true)
                    {
                        accessible = "Yes";
                        Console.WriteLine(detail.Site_Id.ToString().PadRight(11) + detail.Max_Occupancy.ToString().PadRight(15) + accessible.PadRight(15) + detail.Max_Rv_Length.ToString().PadRight(15) + Utilities[detail.Utilities].PadRight(15) + ("$" + (numberOfDays * newCampground.Daily_Fee).ToString("0.00").PadRight(20)));

                    }
                    else if (detail.Accessible == false)
                    {
                        accessible = "No";
                        Console.WriteLine(detail.Site_Id.ToString().PadRight(11) + detail.Max_Occupancy.ToString().PadRight(15) + accessible.PadRight(15) + detail.Max_Rv_Length.ToString().PadRight(15) + Utilities[detail.Utilities].PadRight(15) + "$" + (numberOfDays * newCampground.Daily_Fee).ToString("0.00").PadRight(20));
                    }
                }
                ReserveSiteMenu();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("**** NO RESERVATIONS AVAILABLE ****");
            }
        }


        public void ReserveSiteMenu()
        {

            Console.WriteLine();
            int siteNumber = CLIHelper.GetInteger("What site should be reserved? (enter 0 to cancel)? ");

            if (siteIdList.Contains(siteNumber) == false)
            {
                Console.WriteLine();
                Console.WriteLine("Please select a valid site.");
                ReserveSiteMenu();
            }

            string ReservationName = CLIHelper.GetString("What name should the reservation be made under? ");


            int reservationId = reservationDAO.CreateNewReservation(ReservationName, newReservation.From_Date, newReservation.To_Date, siteNumber);


            Console.WriteLine($"\nThe reservation has been made and the confirmation id is {reservationId}");
            Console.ReadLine();
            Console.Clear();
            PrintMenu();

        }



        private void PrintMenu()
        {

            Console.WriteLine("National Parks Main Menu");
            Console.WriteLine();
            Console.WriteLine("Select a Park for Further Details");
            Console.WriteLine(" 1 - Acadia");
            Console.WriteLine(" 2 - Arches");
            Console.WriteLine(" 3 - Cuyahoga National Valley Park");
            Console.WriteLine(" Q - Quit");
            Console.WriteLine();
            Console.Write("Enter Selection: ");
        }




        public void Welcome()
        {
            Console.Write(" *** ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("W");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("E");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("L");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("C");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("M");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("E");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" *** ");
            Console.WriteLine();
        }

    }
}


