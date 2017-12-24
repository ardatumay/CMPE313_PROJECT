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
    public class PlayerController : Controller
    {
        // GET: Player
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPlayer()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            //ViewBag.Teams = teams;

            var positions = PlayerPersistence.GetPositions();
            ViewData["Positions"] = positions;
            return View(new Player());
        }

        [HttpPost]
        public ActionResult AddPlayer(Player player)
        {
            bool isExist = true;
            if (player == null)
            {
                return View(new Player());
            }
            if (player.name == null || player.name.Length == 0 || player.surname == null || player.surname.Length == 0 || player.birthDate == null || player.birthDate.Length == 0 || player.position == null || player.position.Length == 0 || player.transferFee < 0 || player.transferFee.ToString().Equals(null) || player.salary < 0 || player.salary.ToString().Equals(null) || player.teamName == null || player.teamName.Length == 0 )
            {
                TempData["message"] = "All fields are required.";
                return View(player);
            }

            Team team = TeamPersistance.GetTeam(new Team(player.teamName));
            if (team == null)
            {
                TempData["message"] = "Invalid Team! Please check team name.";
                return View(player);
            }
            else
            {
                player.teamID = team.ID;

            }
            isExist = PlayerManager.CheckPlayer(player);
            if (isExist)
            {
                TempData["message"] = "This player is already exist.";
                return View(player);
            }
            else
            {
                PlayerPersistence.AddPlayer(player);
                TempData["message"] = "Player is added successfully.";
                return RedirectToAction("Index", "Home");
            }
        }
        
        public List<Team> GetTeams ()
        {
            return TeamPersistance.GetTeams();
        }
    }

}