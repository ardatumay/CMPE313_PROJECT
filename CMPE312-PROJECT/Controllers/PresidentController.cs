﻿using System;
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
                return View(new President());
            }

            if (president.name == null || president.name.Length == 0 || president.surname == null || president.surname.Length == 0 || president.birthDate == null || president.birthDate.Length == 0 || president.teamName == null || president.teamName.Length == 0)
            {
                TempData["message"] = "All fields are required.";
                return View(president);
            }

            if (PresidentPersistance.GetPresident(president) != null)
            {
                TempData["message"] = "The president is already exist.";
                return View(president);
            }

            Team team = TeamPersistance.GetTeam(new Team(president.teamName));
            if (team == null)
            {
                TempData["message"] = "Invalid Team! Please check team name.";
                return View(president);
            }

            else
            {
                president.teamID = team.ID;
            }

            isExist = PresidentManager.CheckPresident(president);
            if (isExist)
            {
                TempData["message"] = "This player is already exist.";
                return View(president);
            }

            else
            {
                President presidentDeleted = new President(president.teamID);
                PresidentPersistance.DeletePresident(presidentDeleted);

                PresidentPersistance.AddPresident(president);
                TempData["message"] = "President of " + team.Name + " is changed successfully.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}