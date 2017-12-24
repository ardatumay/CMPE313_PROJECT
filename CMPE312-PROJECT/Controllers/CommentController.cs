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

        /*public ActionResult CommentPlayer()
        {

        }
        public ActionResult CommentCoach()
        {

        }*/

        [HttpGet]
        public ActionResult CommentPresident()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            return View(new Comment());
        }

        [HttpPost]
        public ActionResult CommentPresident(Comment comment)
        {
            return View();
        }

        [HttpGet]
        public ActionResult CommentTeam()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            return View(new Comment());
        }

        [HttpPost]
        public ActionResult CommentTeam(Comment comment)
        {
            bool isAdded;
            if (comment == null)
            {
                return View(new Comment());
            }
            if (comment.teamName == null || comment.teamName.Length == 0 || comment.comment == null || comment.comment.Length == 0)
            {
                TempData["message"] = "All fields are required.";
                return View(comment);
            }
            Team team = TeamPersistance.GetTeam(new Team(comment.teamName));
            if (team == null)
            {
                TempData["message"] = "Invalid Team! Please check team.";
                return View(comment);
            }
            else
            {
                comment.teamID = team.ID;
            }
            isAdded = CommentManager.AddComment(comment);
            TempData["message"] = "Comment is added succesfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}