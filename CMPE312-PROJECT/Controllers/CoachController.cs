using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;
using CMPE312_PROJECT.Models.Persistance;


namespace CMPE312_PROJECT.Controllers
{
    public class CoachController : Controller
    {
        // GET: Coach
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangeCoach()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            return View(new Coach());
        }

        [HttpPost]
        public ActionResult ChangeCoach(Coach coach)
        {

            bool isAdded = true;
            if (coach == null)
            {
                return View(new Player());
            }
            if (coach.Name == null || coach.Name.Length == 0 || coach.Surname == null || coach.Surname.Length == 0 || coach.BirthDate == null || coach.BirthDate.Length == 0 || coach.Salary.ToString() ==  null ||  coach.Salary.ToString().Length == 0 || coach.Salary == 0 || coach.TeamName == null || coach.TeamName.Length == 0)
            {
                TempData["message"] = "All fields are required.";
                return View(coach);
            }
            Team team = TeamPersistance.GetTeam(new Team(coach.TeamName));
            if (team == null)
            {
                TempData["message"] = "Invalid Team! Please check team name.";
                return View(coach);
            }
            else
            {
                coach.TeamID = team.ID;

            }
            coach.ID = 1;
            isAdded = CoachManager.AddCoach(coach);

            if (isAdded)
            {
                TempData["message"] = "Coach is successfully added.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["message"] = "Coach is already registered.";
                return View(coach);
            } 

            return View(new Coach());
        }
    }
}