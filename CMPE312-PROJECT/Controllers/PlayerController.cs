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

            

            if (player.name == null || player.surname == null || player.birthDate == null || player.position == null || player.transferFee < 0 || player.salary < 0)
            {
                TempData["message"] = "Be sure that you filled the required fields correctly.";
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
                isExist = PlayerManager.CheckPlayer(player);
            }

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
            return Models.Persistance.TeamPersistance.GetTeams();
        }
    }

}