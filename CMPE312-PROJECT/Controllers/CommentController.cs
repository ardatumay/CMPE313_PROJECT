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
     * This class is created for providing connection between View and Model about Comment.
     */
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        /*
         * This method is HttpGet method of comment on a player feature for logged in users. It returns a new Comment object.
         */
        [HttpGet]
        public ActionResult CommentPlayer()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            return View(new Comment());
        }

        /*
         * This method is HttpPost method of comment on a player feature for logged in users.
         * It takes a Comment object as parameter and sends this object to Model for adding this comment object to the database.
         */
        [HttpPost]
        public ActionResult CommentPlayer(Comment comment)
        {
            bool isAdded;
            if (comment == null)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "Comment object is null.";
                return View(new Comment());
            }
            if (comment.TeamName == null || comment.TeamName.Length == 0 || comment.CommentValue == null || comment.CommentValue.Length == 0 || comment.PlayerId == 0 ||comment.PlayerId.ToString().Equals(null) || comment.PlayerId.ToString().Length ==0)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "All fields are required.";
                return View(comment);
            }
            string validComment = @"^[a-zA-Z ][a-zA-Z0-9 ]*$";
            Match matchcomment = Regex.Match(comment.CommentValue, validComment);
            if (!matchcomment.Success)
            {
                TempData["message"] = "Incorrect letters";
                return View(comment);
            }
            comment.CommentValue = comment.CommentValue.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            Team team = TeamPersistance.GetTeam(new Team(comment.TeamName));
            comment.TeamID = team.ID;
            isAdded = CommentManager.AddCommentPlayer(comment);
            TempData["message"] = "Comment is added succesfully.";
            return RedirectToAction("Index", "Home");
        }


        /*
         * This method is HttpGet method of comment on a team feature for logged in users. It returns a new Comment object.
         */
        [HttpGet]
        public ActionResult CommentTeam()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            return View(new Comment { TeamName = " ", CommentValue = "comment" });
        }

        /*
         * This method is HttpPost method of comment on a team feature for logged in users.
         * It takes a Comment object as parameter and sends this object to Model for adding this comment object to the database.
         */
        [HttpPost]
        public ActionResult CommentTeam(Comment comment)
        {
            
            bool isAdded;
            if (comment == null)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "Comment object is null.";
                return View(new Comment());
            }
            if (comment.TeamName == null || comment.TeamName.Length == 0 || comment.CommentValue == null || comment.CommentValue.Length == 0)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "All fields are required."+ comment.TeamName +","+ comment.CommentValue;
                return View(comment);
            }
            string validComment = @"^[a-zA-Z ][a-zA-Z0-9 ]*$";
            Match matchcomment = Regex.Match(comment.CommentValue, validComment);
            if (!matchcomment.Success)
            {
                var teams = TeamPersistance.GetTeams();
                ViewData["Teams"] = teams;
                TempData["message"] = "Incorrect letters";
                return View(comment);
            }
            comment.CommentValue = comment.CommentValue.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");

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
            isAdded = CommentManager.AddCommentTeam(comment);
            TempData["message"] = "Comment is added succesfully.";
            return RedirectToAction("Index", "Home");
        }

        /*
         * This method is HttpGet method of list comments of a player feature of the application.
         */
        [HttpGet]
        public ActionResult ListPlayerComments()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            return View();
        }

        /*
         * This method is HttpGet method of list comments of a player feature of the application.
         */
        [HttpGet]
        public string GetPlayerComments(string teamName, decimal playerId)
        {
            Team team = TeamPersistance.GetTeam(new Team(teamName));
            Player player = Player.CreatePlayerById(playerId);
            List<Comment> comments = CommentPersistence.GetPlayerComments(team, player);
            string playerComments = "";

            if (comments == null)
            {
                //TempData["message"] = "There are no comments about the player.";
                return null;
            }
            else
            {
                for (int i = 0; i < comments.Count; i++)
                {
                    playerComments += comments[i].CommentValue + ",";
                }
            }
            return playerComments;
        }

        /*
         * This method is HttpGet method of list comments of a team feature of the application.
         */
        [HttpGet]
        public ActionResult ListTeamComments()
        {
            var teams = TeamPersistance.GetTeams();
            ViewData["Teams"] = teams;
            return View();
        }

        /*
         * This method is HttpGet method of list comments of a team feature of the application.
         */
        [HttpGet]
        public string GetTeamComments(string teamName)
        {

            Team team = TeamPersistance.GetTeam(new Team(teamName));
            List<Comment> comments = CommentPersistence.GetTeamComments(team);
            string teamComments = "";
            if (comments == null)
            {
                //TempData["message"] = "There are no comments about the team.";
                return null;
            }
            else
            {
                for (int i = 0; i < comments.Count; i++)
                {
                    teamComments += comments[i].CommentValue + ",";
                }
            }

            return teamComments;
        }


    }
}