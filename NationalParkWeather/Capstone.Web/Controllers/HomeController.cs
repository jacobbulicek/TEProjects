using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string TEMP_SETTING_SESSION_KEY = "Temp_Setting";
        private IParkDao parkDao;
        private ISurvey_ResultSqlDao survey_ResultDao;

        public HomeController(IParkDao parkDao, ISurvey_ResultSqlDao survey_ResultDao)
        {
            this.parkDao = parkDao;
            this.survey_ResultDao = survey_ResultDao;
        }

        public IActionResult Index()
        {
            IList<Park> parks = parkDao.GetParks();
            return View(parks);
        }


        public IActionResult Detail(string parkCode)
        {

            dynamic mymodel = new ExpandoObject();
            mymodel.Park = parkDao.GetPark(parkCode);

            mymodel.Weather = parkDao.GetParkWeather(parkCode);
          

            if (GetCurrentTempSettings() == "C")

            {
                foreach (ParkWeather weather in mymodel.Weather)
                {
                    weather.Low = (int)((weather.Low - 32.0) * (5.0 / 9.0));
                    weather.High = (int)((weather.High - 32.0) * (5.0 / 9.0));
                }
            }


            return View(mymodel);
        }


        [HttpGet]
        public IActionResult Survey()
        {
            IList<Park> parks = parkDao.GetParks();
            ViewBag.ParkSelectList = parkDao.GetParkSelectList();
            return View();
        }

        [HttpPost]
        public IActionResult Survey(Survey_Result surveyResult)
        {
            surveyResult.State = surveyResult.State.ToUpper();
            survey_ResultDao.AddResult(surveyResult);
            return RedirectToAction("SurveyResults");
        }

        public IActionResult SurveyResults()
        {
            List<SurveyResultViewModel> surveys = new List<SurveyResultViewModel>();
            surveys = survey_ResultDao.GetTopRankedParks();
            return View(surveys);
        }

        // temperature stuff 

        //[HttpGet]
        //public IActionResult Temperature()
        //{
        //    Temperature model = new Temperature();
        //    model.TempSettings = GetCurrentTempSettings();
        //    return View(model);
        //}

        [HttpPost]
        public IActionResult Temperature(string parkCode, string tempSettings)
        {
           
            SaveCurrentTempSettings(tempSettings);
            return RedirectToAction("Detail",new { parkCode = parkCode });
        }

        //[HttpPost]
        ////need to make property in temperature that holds parkcode.
        //public IActionResult Temperature(Temperature model)
        //{
        //    SaveCurrentTempSettings(model.TempSettings);
        //    return RedirectToAction("Detail" , model.ParkCode);
        //}

        private string GetCurrentTempSettings()
        {
            string setting = HttpContext.Session.GetString(TEMP_SETTING_SESSION_KEY);

            if (string.IsNullOrWhiteSpace(setting))
            {
                setting = "F";
                SaveCurrentTempSettings(setting);
            }
            return setting;
        }

        private void SaveCurrentTempSettings(string setting)//this is our f or our c
        {
            HttpContext.Session.SetString(TEMP_SETTING_SESSION_KEY, setting);
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
