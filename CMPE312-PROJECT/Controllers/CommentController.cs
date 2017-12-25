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
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CommentPlayer()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            return View(new Comment());
        }

        [HttpPost]
        public ActionResult CommentPlayer(Comment comment)
        {
            bool isAdded;
            if (comment == null)
            {
                return View(new Comment());
            }
            if (comment.TeamName == null || comment.TeamName.Length == 0 || comment.CommentValue == null || comment.CommentValue.Length == 0)
            {
                TempData["message"] = "All fields are required.";
                return View(comment);
            }
            Team team = TeamPersistance.GetTeam(new Team(comment.TeamName));
            if (team == null)
            {
                TempData["message"] = "Invalid Team! Please check team.";
                return View(comment);
            }
            else
            {
                comment.TeamID = team.ID;
            }
            isAdded = CommentManager.AddComment(comment);
            TempData["message"] = "Comment is added succesfully.";
            return RedirectToAction("Index", "Home");
        }




        [HttpGet]
        public ActionResult CommentTeam()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            return View(new Comment { TeamName = " ", CommentValue = "comment" });
        }

        [HttpPost]
        public ActionResult CommentTeam(Comment comment)
        {
            
            bool isAdded;
            if (comment == null)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "HERE";

                return View(new Comment());
            }
            if (comment.TeamName == null || comment.TeamName.Length == 0 || comment.CommentValue == null || comment.CommentValue.Length == 0)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "All fields are required."+ comment.TeamName +","+ comment.CommentValue;
                return View(comment);
            }
            Team team = TeamPersistance.GetTeam(new Team(comment.TeamName));
            if (team == null)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "Invalid Team! Please check team.";
                return View(comment);
            }
            else
            {
                comment.TeamID = team.ID;
            }
            isAdded = CommentManager.AddComment(comment);
            TempData["message"] = "Comment is added succesfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}