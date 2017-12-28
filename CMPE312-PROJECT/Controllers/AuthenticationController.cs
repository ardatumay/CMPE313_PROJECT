﻿using System;
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
            string validUserId = @"^[A-Za-z.][A-Za-z0-9\-.]*$";
            string validPass = @"^[a-z0-9!@#$*]{8,12}$";
            if (credential == null)
            {
                return View(new Credential());
            }

            User user = UserPersistence.GetUser((credential.UserId));

            if (user == null)
            {
                TempData["message"] = "User was not found.";
                return View(credential);
            }

            if ((credential.UserId == null) || (credential.UserId.Length == 0) || (credential.Password1 == null) || (credential.Password1.Length == 0))
            {
                TempData["message"] = "Both user id and password are required";
                return View(credential);
            }

            else if (user.Status.Equals("I"))
            {
                TempData["message"] = "This user is inactive. Please contact with admins.";
                return View(credential);
            }

            else
            {
                Match matchid = Regex.Match(credential.UserId, validUserId);
                Match matchpass = Regex.Match(credential.Password1, validPass);
                if (!matchid.Success || !matchpass.Success)
                {
                    TempData["message"] = "Incorrect letters";
                    return View(credential);
                }
                credential.UserId = credential.UserId.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
                credential.Password1 = credential.Password1.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");

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
            string validUserId = @"^[A-Za-z][A-Za-z0-9\-]*$";
            string validPass = @"^[a-z0-9!@#$*]{8,12}$";
            string validName = @"^[a-zA-Z ][a-zA-Z0-9 ]*$";
            string validMail = @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+.[A-Za-z0-9]+";

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
                Match matchid = Regex.Match(credential.UserId, validUserId);
                Match matchname = Regex.Match(credential.Name, validName);
                Match matchpass = Regex.Match(credential.Password1, validPass);
                Match matchmail = Regex.Match(credential.Email, validMail);

                if (!matchid.Success || !matchpass.Success || !matchname.Success || !matchmail.Success)
                {
                    TempData["message"] = "Incorrect letters";
                    return View(credential);
                }
                credential.UserId = credential.UserId.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
                credential.Password1 = credential.Password1.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
                credential.Password2 = credential.Password2.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
                credential.Name = credential.Name.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
                credential.Email = credential.Email.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");

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
            return View(new Credential { UserId = user.UserID, Name = user.Name, Email = user.Email });
        }

        [HttpPost]
        public ActionResult GetUserInfo(Credential credential)
        {
            string validUserId = @"^[A-Za-z][A-Za-z0-9\-]*$";
            string validPass = @"^[a-z0-9!@#$*]{8,12}$";
            string validName = @"^[a-zA-Z][a-zA-Z0-9]*$";
            string validMail = @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+.[A-Za-z0-9]+";

            bool login;
            string newPassword = credential.Password1;

            if (credential == null)
            {
                return View(new Credential());
            }

            if (((credential.Name == null) || (credential.Name.Length == 0)) && ((credential.Email == null) || (credential.Email.Length == 0)))
            {
                TempData["message"] = "Nothing has changed.";
                User user1 = UserManager.user;
                return View(new Credential { UserId = user1.UserID, Name = user1.Name, Email = user1.Email });
            }

            /*if (credential.Password1.Equals(credential.OldPassword))
            {
                TempData["message"] = "Your new password cannot be the same with your current password!";
                return View(credential);
            }

            else if (!credential.Password1.Equals(credential.Password2))
            {
                TempData["message"] = "Passwords are not same.";
                return View(credential);
            }
            */
            if (credential.Name != null && credential.Email != null)
            {
                Match matchname = Regex.Match(credential.Name, validName);
                Match matchmail = Regex.Match(credential.Email, validMail);

                if (!matchname.Success && !matchmail.Success)
                {
                    TempData["message"] = "Invalid Name and Email!";
                    User user1 = UserManager.user;
                    return View(new Credential { UserId = user1.UserID, Name = user1.Name, Email = user1.Email });
                }
            }


            if (credential.Name != null)
            {
                Match matchname = Regex.Match(credential.Name, validName);

                if (!matchname.Success)
                {
                    TempData["message"] = "Invalid Name!";
                    User user1 = UserManager.user;
                    return View(new Credential { UserId = user1.UserID, Name = user1.Name, Email = user1.Email });
                }

                else
                {
                    User user = UserManager.user;
                    user.Name = credential.Name;
                    UserManager.UpdateUser(user);
                }
            }

            if (credential.Email != null)
            {
                Match matchmail = Regex.Match(credential.Email, validMail);

                if (!matchmail.Success)
                {
                    TempData["message"] = "Invalid Email!";
                    User user1 = UserManager.user;
                    return View(new Credential { UserId = user1.UserID, Name = user1.Name, Email = user1.Email });
                }

                else
                {
                    User user = UserManager.user;
                    user.Email = credential.Email;
                    UserManager.UpdateUser(user);
                }
            }

            TempData["message"] = "Changes saved successfully.";
            User user3 = UserManager.user;
            return View(new Credential { UserId = user3.UserID, Name = user3.Name, Email = user3.Email });

        }

        [HttpGet]
        public ActionResult UserChangePassword()
        {
            User user = UserManager.user;
            return View(new Credential ());
        }

        [HttpPost]
        public ActionResult UserChangePassword(Credential credential)
        {
            string validPass = @"^[a-z0-9!@#$*]{8,12}$";
            string validName = @"^[a-zA-Z ][a-zA-Z0-9 ]*$";
            string validMail = @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+.[A-Za-z0-9]+";

            bool login;
            string newPassword = credential.Password1;

            if (credential == null)
            {
                return View(new Credential());
            }

            if ((credential.Password1 == null) || (credential.Password1.Length == 0) || (credential.Password2 == null) || (credential.Password2.Length == 0) || (credential.OldPassword == null) || (credential.OldPassword.Length == 0))
            {
                TempData["message"] = "All fields are required.";
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
                Match matchpass = Regex.Match(credential.Password1, validPass);

                if (!matchpass.Success)
                {
                    TempData["message"] = "Incorrect letters";
                    return View(credential);
                }
                credential.UserId = UserManager.user.UserID;
                credential.Password1 = credential.OldPassword;
                login = UserManager.AuthenticateUser(credential, Session);
            }

            if (login)
            {
                credential.Password1 = newPassword;
                credential.Name = UserManager.user.Name;
                credential.Email = UserManager.user.Email;
                credential.IsAdmin = UserManager.user.IsAdmin;
                credential.IsPresident = UserManager.user.PresidentID;

                UserManager.UpdateUser(credential, Session);
                TempData["message"] = "Changes saved successfully.";
                return RedirectToAction("GetUserInfo", "Authentication");
            }

            else
            {
                TempData["message"] = "Invalid login credentials";
                return View(new Credential());
            }

        }

        [HttpGet]
        public ActionResult ChangeStatus()
        {
            List<User> users = UserPersistence.GetAllUsers();
            ViewData["Users"] = users;
            return View(new User());
        }

        [HttpPost]
        public ActionResult ChangeStatus(User user_)
        {
            bool updated;
            User user = UserManager.getUserById(user_.UserID);
            if(user_.Status == null || user_.Status.Length == 0 || user_.UserID == null || user_.UserID.Length == 0)
            {
                List<User> users = UserPersistence.GetAllUsers();
                ViewData["Users"] = users;
                TempData["message"] = "All fields are required.";
                return View(user_);
            }
            if(user_.Status == user.Status)
            {
                List<User> users = UserPersistence.GetAllUsers();
                ViewData["Users"] = users;
                string status = user_.Status == "A" ? "active" : "Inactive";
                TempData["message"] = "This User account is already " + status;
                return View(user_);
            }
            else
            {
                user.Status = user_.Status;
                updated = UserManager.UpdateUser(user);
            }
            if (updated)
            {
                TempData["message"] = "User status is changed.";
                return RedirectToAction("Index", "Home");
            }else
            {
                TempData["message"] = "User status cannot chaned.";
                return View(user_);

            }
        }

        [HttpGet]
        public string GetUserStatus(string UserId)
        {
            User user = UserManager.getUserById(UserId);
            return user.Status == "A" ? "Active" : "Inactive";
        }


        [HttpGet]
        public ActionResult ChangeUserPassword()
        {
            List<User> users = UserPersistence.GetAllUsers();
            ViewData["Users"] = users;
            return View(new Credential());
        }

        [HttpGet]
        public ActionResult UserInformation()
        {
            ViewData["Users"] = UserPersistence.GetAllUsers();
            return View(new User());
        }

        [HttpGet]
        public ActionResult Statistics()
        {
            ViewData["Inactive"] = UserPersistence.GetNumberOfInactive();
            ViewData["Active"] = UserPersistence.GetNumberOfActive();
            ViewData["UserNumber"] = UserPersistence.GetNumberOfUsers();
            ViewData["TeamNumber"] = TeamPersistance.GetNumberOfTeams();
            ViewData["PlayerNumber"] = PlayerPersistence.GetNumberOfPlayers();
            ViewData["CoachNumber"] = CoachPersistence.GetNumberOfCoaches();
            ViewData["PresidentNumber"] = PresidentPersistance.GetNumberOfPresident();
            ViewData["CommentNumber"] = CommentPersistence.GetNumberOfComments();
            return View();
        }

        [HttpPost] 
        public ActionResult ChangeUserPassword(Credential cre)
        {
            if(cre == null)
            {
                List<User> users = UserPersistence.GetAllUsers();
                ViewData["Users"] = users;
                TempData["message"] = "User is null";
                return View(new Credential());
            }
            if(cre.UserId == null || cre.UserId.Length == 0 || cre.Password1 == null || cre.Password1.Length == 0)
            {
                List<User> users = UserPersistence.GetAllUsers();
                ViewData["Users"] = users;
                TempData["message"] = "All fields are required.";
                return View(cre);
            }
            string validPass = @"^[a-z0-9!@#$*]{8,12}$";
            Match matchpass = Regex.Match(cre.Password1, validPass);
            if (!matchpass.Success)
            {
                List<User> users = UserPersistence.GetAllUsers();
                ViewData["Users"] = users;
                TempData["message"] = "Incorrect letters";
                return View(cre);
            }
            cre.Password1 = cre.Password1.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            User user = UserManager.getUserById(cre.UserId);
            bool updated = UserManager.ChangeUserPassword(user, cre);
            if (updated)
            {
                TempData["message"] = "User password is changed successfully. \n New password is set to "+ cre.Password1;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<User> users = UserPersistence.GetAllUsers();
                ViewData["Users"] = users;
                TempData["message"] = "A problem occured during password change.";
                return View(cre);
            }
        }

    }
}