using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.;

namespace Lab7.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new Credential());
        }

        [HttpPost]
        public ActionResult Login(Credential credential)
        {
            bool login;
            if (credential == null)
            {
                return View(new Credential());
            }
            if ((credential.UserId == null) || (credential.UserId.Length == 0) || (credential.Password == null) || (credential.Password.Length == 0))
            {
                TempData["message"] = "Both user id and password are required";
                return View(credential);
            }
            else
            {
                login = UserManager.AuthenticateUser(credential, Session);
            }
            if (login)
            {
                TempData["message"] = "Login Successful";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["message"] = "Invalid login credentials";
                return View(credential);
            }    
        }
        public ActionResult Logout()
        {
            UserManager.LogoutUSer(Session);
            TempData["message"] = "Logged out";
            return RedirectToAction("Index", "Home");
        }
    }
}