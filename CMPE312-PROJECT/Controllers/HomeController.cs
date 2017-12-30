using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Transaction;
using CMPE312_PROJECT.Models.Persistance;
using System.Text.RegularExpressions;

namespace CMPE312_PROJECT.Controllers
{
    /*
     * This class is created for providing connection between View and Model.
     */
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var teams = TeamPersistance.GetTeamsByPointOrder();
            ViewData["Teams"] = teams;
            return View("Index", new Team());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public string GetTeamInfo(string teamName)
        {
            Team team1 = Team.CreateTeamWithName(teamName);
            Team team2 = TeamPersistance.GetTeam(team1);
            return team2.GetTeamInfo();
        }

    }
}