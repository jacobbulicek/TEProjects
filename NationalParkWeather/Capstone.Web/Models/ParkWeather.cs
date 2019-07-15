using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkWeather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }
        public string WeatherImage { get; set; }
        public string WeatherReccomendation { get; set; }
        public string TempRecommendation { get; set; }


        public string GetWeatherImage()
        {

            string weatherImage = "";
            if (Forecast == "rain")
            {
                weatherImage = "rain.png";
            }
            else if (Forecast == "cloudy")
            {
                weatherImage = "cloudy.png";
            }
            else if (Forecast == "partly cloudy")
            {
                weatherImage = "partlyCloudy.png";
            }
            else if (Forecast == "snow")
            {
                weatherImage = "snow.png";
            }
            else if (Forecast == "sunny")
            {
                weatherImage = "sunny.png";
            }
            else if (Forecast == "thunderstorms")
            {
                weatherImage = "thunderstorms.png";
            }
            return weatherImage;
        }

        public string GetWeatherRec()
        {
            string weatherRec = "";
            if (Forecast == "rain")
            {
                weatherRec = "Better pack your rain gear and wear waterproof shoes!";
            }

            else if (Forecast == "snow")
            {
                weatherRec = "Be sure to bring snowshoes";
            }
            else if (Forecast == "sunny")
            {
                weatherRec = "Pack sunscreen";
            }
            else if (Forecast == "thunderstorms")
            {
                weatherRec = "Seek shelter. Avoid Exposed Ridges.";
            }
        
         
            return weatherRec;

        }

        public string GetTempRec()
        {
            string tempRec = "";

            if (High > 75)
            {
                tempRec = "Bring an extra gallon of water.";
            }
            else if (High - Low > 20)
            {
                tempRec = "Wear breathable layers.";
            }
            else if (Low < 20)
            {
                tempRec = "Beware! Exposure to fridgid temperatures is dangerous!";
            }
            return tempRec;
        }
    }

}


