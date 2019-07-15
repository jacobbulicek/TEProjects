using System;
using Capstone.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface ISurvey_ResultSqlDao
    {
        List<Survey_Result> GetAllSurveys();
        bool AddResult(Survey_Result result);
        List<SurveyResultViewModel> GetTopRankedParks();
    }
}
