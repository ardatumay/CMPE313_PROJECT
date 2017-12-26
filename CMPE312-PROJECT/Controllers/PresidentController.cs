using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;
using CMPE312_PROJECT.Models.Persistance;
using System.Text.RegularExpressions;


namespace CMPE312_PROJECT.Controllers
{
    public class PresidentController : Controller
    {
        // GET: President
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePresident()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            //ViewBag.Teams = teams;
            return View(new President());
        }

        [HttpPost]
        public ActionResult ChangePresident(President president)
        {
            bool isExist = true;
            if (president == null)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                return View(new President());
            }

            if (president.Name == null || president.Name.Length == 0 || president.Surname == null || president.Surname.Length == 0 || president.BirthDate == null || president.BirthDate.Length == 0 || president.TeamName == null || president.TeamName.Length == 0)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "All fields are required.";
                return View(president);
            }
            string validName = @"^[a-zA-Z ][a-zA-Z0-9 ]*$";
            Match matchname = Regex.Match(president.Name, validName);
            Match matchsurname = Regex.Match(president.Surname, validName);
            if (!matchname.Success || !matchsurname.Success )
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "Incorrect letters";
                return View(president);
            }
            if (PresidentPersistance.GetPresident(president) != null)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "The president is already exist.";
                return View(president);
            }

            Team team = TeamPersistance.GetTeam(new Team(president.TeamName));
            if (team == null)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "Invalid Team! Please check team name.";
                return View(president);
            }

            else
            {
                president.TeamID = team.ID;
            }

            isExist = PresidentManager.CheckPresident(president);
            if (isExist)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "This player is already exist.";
                return View(president);
            }

            else
            {
                President presidentDeleted = new President(president.TeamID);
                PresidentPersistance.DeletePresident(presidentDeleted);

                PresidentPersistance.AddPresident(president);
                TempData["message"] = "President of " + team.Name + " is changed successfully.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}