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
    public class TeamController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddTeam()
        {
            return View(new Team());
        }

        [HttpPost]
        public ActionResult AddTeam(Team team)
        {
            if (team == null)
            {
                return View(new Team());
            }
            if ((team.Name == null) || (team.Name.Length == 0) || (team.City== null) || (team.City.Length == 0) || (team.Foundation.ToString().Equals(null)) || (team.Foundation.ToString().Length == 0) || (team.Foundation.ToString().Length > 4) || (team.Budget.ToString().Equals(null)) || (team.Budget.ToString().Length == 0) || (team.NumberOfChampionship.ToString().Equals(null)) || (team.NumberOfChampionship.ToString().Length == 0))
            {
                if(team.Foundation.ToString().Length > 4)
                {
                    TempData["message"] = "Foundation year must be 4 characters.";
                    return View(team);
                }
                TempData["message"] = "All fields are required.";
                return View(team);
            }
            string validName = @"^[a-zA-Z ][a-zA-Z0-9 ]*$";
            Match matchname = Regex.Match(team.Name, validName);
            Match matchcity = Regex.Match(team.City, validName);
            if (!matchname.Success || !matchcity.Success)
            {
                TempData["message"] = "Incorrect letters";
                return View(team);
            }
            team.Name = team.Name.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            team.City = team.City.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            bool isAdded = TeamManager.AddTeam(team);
            if (isAdded)
            {
                TempData["message"] = "Team is successfully added.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["message"] = "Team is already registered.";
                return View(team);
            }   
        }

        [HttpGet]
        public ActionResult DeleteTeam()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            return View(new Team());
        }

        [HttpPost]
        public ActionResult DeleteTeam(Team team)
        {
            if (team == null)
            {
                return View(new Team());
            }
            if ((team.Name == null) || (team.Name.Length == 0))
            {
                TempData["message"] = "All fields are required.";
                return View(team);
            }

            Team team1 = TeamPersistance.GetTeam(team);
            if (team1 != null)
            {
                TeamPersistance.DeleteTeam(team1);
                TempData["message"] = team1.Name + " is successfully deleted.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["message"] = "This team is not exist. Please check the fields.";
                return View(team);
            }
        }

        [HttpGet]
        public string GetTeamPlayers(string teamName)
        {
            Team team = TeamPersistance.GetTeam(new Team(teamName));
            List<Player> teamPlayers = TeamPersistance.GetTeamPlayers(team);
            string playerList = null;
            foreach(Player player in teamPlayers)
            {
                playerList += player.ID.ToString()+","+player.Name+",";
            }
            return playerList;
        }

        [HttpGet]
        public ActionResult ChangeBudget()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;

            Team team = TeamPersistance.GetTeamByID(new Team { ID = UserManager.user.PresidentID });
            ViewData["TeamName"] = team.Name;
            
            return View(team);
        }

        [HttpPost]
        public ActionResult ChangeBudget(Team team)
        {
            if (team == null)
            {
                return View(new Team());
            }

            Team team1 = TeamPersistance.GetTeamByID(new Team { ID = UserManager.user.PresidentID });
            team1.Budget = team.Budget;
            team = team1;

            if ((team.Name == null) || (team.Name.Length == 0))
            {
                TempData["message"] = "All fields are required.";

                team = TeamPersistance.GetTeamByID(new Team { ID = UserManager.user.PresidentID });
                ViewData["TeamName"] = team.Name;

                return View(team);
            }

            TeamPersistance.UpdateTeam(team1);
            TempData["message"] = "Budget of " + team.Name + " is successfully changed.";
            return RedirectToAction("Index", "Home");

        }
    }
}