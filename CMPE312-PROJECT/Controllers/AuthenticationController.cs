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
    /*
     * This class is created for providing connection between View and Model about User and Credential.
     */
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        /*
         * This method is HttpGet method of login feature of the application. It returns a new Credential object.
         */
        [HttpGet]
        public ActionResult Login()
        {
            return View(new Credential());
        }

        /*
         * This method is HttpPost method of login feature of the application. 
         * It takes a Credential object as parameter and send this object to Model for authentication.
         */
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(Credential credential)
        {
            bool login;
            string validUserId = @"^[A-Za-z.][A-Za-z0-9\-.]*$";
            string validPass = @"^[a-z0-9!@#$*]{8,12}$";
            if (credential == null)
            {
                return View(new Credential());
            }

            if ((credential.UserId == null) || (credential.UserId.Length == 0) || (credential.Password1 == null) || (credential.Password1.Length == 0))
            {
                TempData["message"] = "Both user id and password are required";
                return View(credential);
            }
            string checkUserID = credential.UserId.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            string checkUserPass = credential.Password1.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");

            if ((!checkUserID.Equals(credential.UserId)) || (!checkUserPass.Equals(credential.Password1)))
            {
                TempData["message"] = "XSS attack found!";
                return View(new Credential());
            }

            User user = UserPersistence.GetUser((credential.UserId));

            if (user == null)
            {
                TempData["message"] = "User was not found.";
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

        /*
         * This method is HttpGet method of signup feature of the application. It returns a new Credential object.
         */
        [HttpGet]
        public ActionResult Signup()
        {
            return View(new Credential());
        }

        /*
         * This method is HttpPost method of login feature of the application.
         * It takes a Credential object as parameter and sends this object to Model for adding that user to database if everything is valid.
         */
        [HttpPost]
        [ValidateInput(false)]
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

            string checkUserID = credential.UserId.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            string checkUserPass1 = credential.Password1.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            string checkUserPass2 = credential.Password2.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            string checkUserName = credential.Name.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            string checkUserMail = credential.Email.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            if ((!checkUserID.Equals(credential.UserId)) || (!checkUserPass1.Equals(credential.Password1)) || !checkUserPass2.Equals(credential.Password2) || !checkUserName.Equals(credential.Name) || !checkUserMail.Equals(credential.Email))
            {
                TempData["message"] = "XSS attack found!";
                return View(new Credential());
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

        /*
         * This method provides logout feature to the application by sending current user object to LogoutUser() method of UserManager class.
         */
        public ActionResult Logout()
        {
            UserManager.LogoutUSer(Session);
            TempData["message"] = "Logged out";
            return RedirectToAction("Index", "Home");
        }

        /*
         * This method is HttpGet method of "get user information" feature of the application. It returns a new Credential object which is created by current user's information.
         */
        [HttpGet]
        public ActionResult GetUserInfo()
        {
            User user = UserManager.user;
            return View(new Credential { UserId = user.UserID, Name = user.Name, Email = user.Email });
        }

        /*
         * This method is HttpPost method of "get user information" feature of the application.
         * It takes a Credential object as parameter and sends this object to Model for updating current user's information if everything is valid.
         */
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GetUserInfo(Credential credential)
        {

            string validName = @"^[a-zA-Z ][a-zA-Z0-9 ]*$";
            string validMail = @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+.[A-Za-z0-9]+";

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

            string checkUserID = credential.UserId.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            string checkEmail = credential.Email.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");

            if (!(checkUserID.Equals(credential.UserId)) || !(checkEmail.Equals(credential.Email)))
            {
                TempData["message"] = "XSS attack found!";
                return View(new Credential());
            }

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
                    credential.Name = credential.Name.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
                    credential.Email = credential.Email.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");

                    User user = UserManager.user;
                    user.Email = credential.Email;
                    UserManager.UpdateUser(user);
                }
            }

            TempData["message"] = "Changes saved successfully.";
            User user3 = UserManager.user;
            return View(new Credential { UserId = user3.UserID, Name = user3.Name, Email = user3.Email });

        }

        /*
         * This method is HttpGet method of change password feature of the application. It returns a new Credential object.
         */
        [HttpGet]
        public ActionResult UserChangePassword()
        {
            User user = UserManager.user;
            return View(new Credential ());
        }

        /*
         * This method is HttpPost method of change password feature of the application.
         * It takes a Credential object as parameter and sends this object to Model for updating current user's password if everything is valid.
         */
        [HttpPost]
        [ValidateInput(false)]
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
            string checkUserOldPass = credential.OldPassword.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            string checkUserPass1 = credential.Password1.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            string checkUserPass2 = credential.Password2.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            if ((!checkUserOldPass.Equals(credential.UserId)) || (!checkUserPass1.Equals(credential.Password1)) || !checkUserPass2.Equals(credential.Password2))
            {
                TempData["message"] = "XSS attack found!";
                return View(new Credential());
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
                Match matchOldpass = Regex.Match(credential.OldPassword, validPass);

                if (!matchpass.Success || !matchOldpass.Success)
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

        /*
         * This method is HttpGet method of change status feature of the application. It returns a new User object.
         */
        [HttpGet]
        public ActionResult ChangeStatus()
        {
            List<User> users = UserPersistence.GetAllUsers();
            ViewData["Users"] = users;
            return View(new User());
        }

        /*
         * This method is HttpPost method of change status feature of the application.
         * It takes a User object as parameter and sends this object to Model for updating current user's status if everything is valid.
         */
        [HttpPost]
        public ActionResult ChangeStatus(User user_)
        {
            bool updated;
            User user = UserManager.getUserById(user_.UserID);
            if(user_.Status == null || user_.Status.Length == 0 || user_.UserID == null || user_.UserID.Length == 0 || user_.UserID == "-" || user_.UserID.Length == 0)
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

        /*
         * This method is HttpGet method of get user status feature of the application.
         */
        [HttpGet]
        public string GetUserStatus(string UserId)
        {
            User user = UserManager.getUserById(UserId);
            return user.Status == "A" ? "Active" : "Inactive";
        }

        /*
         * This method is HttpGet method of "Information for all users" feature of the application. It returns a new User object.
         */
        [HttpGet]
        public ActionResult UserInformation()
        {
            ViewData["Users"] = UserPersistence.GetAllUsers();
            return View(new User());
        }

        /*
         * This method is HttpGet method of statistics feature of the application.
         */
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

        /*
         * This method is HttpGet method of change password feature for admins of the application. It returns a new Credential object.
         */
        [HttpGet]
        public ActionResult ChangeUserPassword()
        {
            List<User> users = UserPersistence.GetAllUsers();
            ViewData["Users"] = users;
            return View(new Credential());
        }

        /*
         * This method is HttpPost method of change password feature for admins of the application.
         * It takes a Credential object as parameter and sends this object to Model for updating selected user's password if everything is valid.
         */
        [HttpPost] 
        [ValidateInput(false)]
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
            string checkUserPass1 = cre.Password1.Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "&#40").Replace(")", "&#41").Replace("&", "&#38").Replace("|", "&#124");
            if ((!checkUserPass1.Equals(cre.Password1)))
            {
                List<User> users = UserPersistence.GetAllUsers();
                ViewData["Users"] = users;
                TempData["message"] = "XSS attack found!";
                return View(new Credential());
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