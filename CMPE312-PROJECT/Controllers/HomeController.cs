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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var teams = TeamPersistance.GetTeams();
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

    }
}