using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;

namespace CMPE312_PROJECT.Controllers
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
            if ((credential.UserId == null) || (credential.UserId.Length == 0) || (credential.Password1 == null) || (credential.Password1.Length == 0))
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
        [HttpGet]
        public ActionResult Signup()
        {
            return View(new Credential());
        }

        [HttpPost]
        public ActionResult Signup(Credential credential)
        {
            bool signup;
            if (credential == null)
            {
                return View(new Credential());
            }
            if ((credential.UserId == null) || (credential.UserId.Length == 0) || (credential.Password1 == null) || (credential.Password1.Length == 0)|| (credential.Password2 == null) || (credential.Password2.Length == 0) || (credential.Email == null) || (credential.Email.Length == 0) || (credential.Name == null) || (credential.Name.Length == 0))
            {
                TempData["message"] = "All inputs are required.";
                return View(credential);
            }
            else if (!credential.Password1.Equals(credential.Password2))
            {
                TempData["message"] = "Passwords are not same.";
                return View(credential);
            }
            else
            {
                signup = UserManager.SignupUser(credential);
                if(signup == false)
                {
                    TempData["message"] = "User already exists.";
                    return View(credential);
                }
                else
                {
                    TempData["message"] = "You are registered. You can login.";
                    return RedirectToAction("Index", "Home");
                }
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