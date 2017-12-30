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
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;

                var positions = PlayerPersistence.GetPositions();
                ViewData["Positions"] = positions;
                return View(new Player());
            }
            if (player.Name == null || player.Name.Length == 0 || player.Surname == null || player.Surname.Length == 0 || player.BirthDate == null || player.BirthDate.Length == 0 || player.Position == null || player.Position.Length == 0 || player.TransferFee < 0 ||  player.TransferFee == 0 || player.TransferFee.ToString().Equals(null) || player.Salary < 0 || player.Salary == 0 || player.Salary.ToString().Equals(null) || player.TeamName == null || player.TeamName.Length == 0 || player.TeamName == "-" )
            {

                if(player.Salary == 0 ||player.TransferFee == 0)
                {
                    var teams = TeamPersistance.GetTeams();
                    ViewData["Teams"] = teams;

                    var positions = PlayerPersistence.GetPositions();
                    ViewData["Positions"] = positions;
                    TempData["message"] = "Salary and transfer fee cannot be 0.";
                    return View(player);
                }
                else
                {
                    var teams = TeamPersistance.GetTeams();
                    ViewData["Teams"] = teams;

                    var positions = PlayerPersistence.GetPositions();
                    ViewData["Positions"] = positions;
                    TempData["message"] = "All fields are required.";
                    return View(player);
                }

            }

            string validName = @"^[a-zA-Z ][a-zA-Z0-9 ]*$";
            Match matchname = Regex.Match(player.Name, validName);
            Match matchsurname = Regex.Match(player.Surname, validName);
            if (!matchname.Success || !matchsurname.Success)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                var positions = PlayerPersistence.GetPositions();
                ViewData["Positions"] = positions;
                TempData["message"] = "Incorrect letters";
                return View(player);
            }
            player.Name = player.Name.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            player.Surname = player.Surname.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");

            Team team = TeamPersistance.GetTeam(new Team(player.TeamName));
            if (team == null)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;

                var positions = PlayerPersistence.GetPositions();
                ViewData["Positions"] = positions;
                TempData["message"] = "Invalid Team! Please check team name.";
                return View(player);
            }
            else
            {
                player.TeamID = team.ID;

            }
            isExist = PlayerManager.CheckPlayer(player);
            if (isExist)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;

                var positions = PlayerPersistence.GetPositions();
                ViewData["Positions"] = positions;
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

        [HttpGet]
        public ActionResult DeletePlayer()
        {
            return View(new Player());
        }
        
        [HttpPost]
        public ActionResult DeletePlayer (Player player)
        {
            bool isExist = true;
            if (player == null)
            {
                return View(new Player());
            }
            if (player.Name == null || player.Name.Length == 0 || player.Surname == null || player.Surname.Length == 0 )
            {
                TempData["message"] = "All fields are required.";
                return View(player);
            }

            isExist = PlayerManager.CheckPlayer(player);
            Player player1 = PlayerPersistence.GetPlayer(player);

            if (isExist)
            {
                PlayerPersistence.DeletePlayer(player1);
                TempData["message"] = player1.Name + " " + player1.Surname + " is deleted successfully.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["message"] = "This player is not exist. Please check the fields.";
                return View(player);
            }
        }


        public List<Team> GetTeams ()
        {
            return TeamPersistance.GetTeams();
        }
    }

}