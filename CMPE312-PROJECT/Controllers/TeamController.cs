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
    public class TeamController : Controller
    {
        // GET: Team
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
            if ((team.Name == null) || (team.Name.Length == 0) || (team.City== null) || (team.City.Length == 0) || (team.Foundation.ToString().Equals(null)) || (team.Foundation.ToString().Length == 0) || (team.Budget.ToString().Equals(null)) || (team.Budget.ToString().Length == 0) || (team.NumberOfChampionship.ToString().Equals(null)) || (team.NumberOfChampionship.ToString().Length == 0))
            {
                TempData["message"] = "All fields are required.";
                return View(team);
            }
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
    }
}