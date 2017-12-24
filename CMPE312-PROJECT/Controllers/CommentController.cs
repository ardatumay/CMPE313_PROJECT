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

        }
        public ActionResult CommentPresident()
        {

        }*/

        [HttpGet]
        public ActionResult CommentTeam()
        {
            var teams = TeamPersistance.GetTeams();
            ViewBag.Teams = teams;
            return View();
        }

        /*[HttpPost]
        public ActionResult CommentTeam()
        {

        }*/
    }
}