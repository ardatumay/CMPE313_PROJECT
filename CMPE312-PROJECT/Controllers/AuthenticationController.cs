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
                TempData["message"] = "All fields are required.";
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

        [HttpGet]
        public ActionResult GetUserInfo()
        {
            User user = UserManager.user;
            return View(user);
        }

        [HttpPost]
        public ActionResult GetUserInfo(User user)
        {
            return RedirectToAction("ChangeUserInfo", "Authentication");
        }

        [HttpGet]
        public ActionResult ChangeUserInfo()
        {
            return View(new Credential());
        }

        [HttpPost]
        public ActionResult ChangeUserInfo(Credential credential)
        {
            bool login;
            string newPassword = credential.Password1;

            if (credential == null)
            {
                return View(new Credential());
            }

            if ((credential.UserId == null) || (credential.UserId.Length == 0) || (credential.Password1 == null) || (credential.Password1.Length == 0))
            {
                TempData["message"] = "Both user id and password are required";
                return View(credential);
            }

            if (credential.Password1.Equals(credential.OldPassword))
            {
                TempData["message"] = "Your new password cannot be the same with your current password!";
                return View(credential);
            }

            else if (!credential.Password1.Equals(credential.Password2))
            {
                TempData["message"] = "Passwords are not same.";
                return View(credential);
            }

            else
            {
                credential.Password1 = credential.OldPassword;
                login = UserManager.AuthenticateUser(credential, Session);
            }

            if (login)
            {
                credential.Password1 = newPassword;
                UserManager.UpdateUser(credential, Session);
                TempData["message"] = "Changes saved successfully.";
                return RedirectToAction("GetUserInfo", "Authentication");
            }

            else
            {
                TempData["message"] = "Invalid login credentials";
                return View(credential);
            }

        }
    }
}