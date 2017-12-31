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
    /*
     * This class is created for providing connection between View and Model about President.
     */
    public class PresidentController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        /*
         * This method is HttpGet method of change president feature for admins of the application. It returns a new President object.
         */
        [HttpGet]
        public ActionResult ChangePresident()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            //ViewBag.Teams = teams;
            return View(new President());
        }

        /*
         * This method is HttpPost method of change president feature for admins of the application.
         * It takes a President object as parameter and sends this object to Model for updating current president.
         */
        [HttpPost]
        [ValidateInput(false)]
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
            if (!matchname.Success || !matchsurname.Success)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "Incorrect letters";
                return View(president);
            }
            string CheckName = president.Name.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            string CheckSurname = president.Surname.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");

            if (!CheckName.Equals(president.Name) || !CheckSurname.Equals(president.Surname))
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "XSS attack found!";
                return View(new President());
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