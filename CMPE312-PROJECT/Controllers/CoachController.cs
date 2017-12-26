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
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                return View(new Player());
            }
            if (coach.Name == null || coach.Name.Length == 0 || coach.Surname == null || coach.Surname.Length == 0 || coach.BirthDate == null || coach.BirthDate.Length == 0 || coach.Salary.ToString() ==  null ||  coach.Salary.ToString().Length == 0 || coach.Salary == 0 || coach.TeamName == null || coach.TeamName.Length == 0)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "All fields are required.";
                return View(coach);
            }
            string validName = @"^[a-zA-Z ][a-zA-Z0-9 ]*$";
            string validsalary = @"/^[0 - 9 +] *$/";
            Match matchname = Regex.Match(coach.Name, validName);
            Match matchsurname = Regex.Match(coach.Surname, validName);
            Match matchsalary = Regex.Match(coach.Salary.ToString(), validsalary);

            if (!matchname.Success || !matchsurname.Success )
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "Incorrect letters";
                return View(coach);
            }
            Team team = TeamPersistance.GetTeam(new Team(coach.TeamName));
            if (team == null)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
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
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "Coach is already registered.";
                return View(coach);
            } 

            return View(new Coach());
        }
    }
}