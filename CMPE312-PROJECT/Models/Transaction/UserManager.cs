﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;


namespace CMPE312_PROJECT.Models.Transaction
{
    public class UserManager
    {
        public static bool AuthenticateUser(Credential cre, HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;
            User user = UserPersistence.GetUser(cre.UserId);
            if(user == null) { 
            return false;
            }
            string passHash = EncryptionManager.EncodePassword(cre.Password1, user.Salt);
            if(passHash != user.PasswordHash)
            {
                return false;
            }
            else
            {
                session["LoggedIn"] = true;
                session["IsAdmin"] = user.IsAdmin;
                return true;
            }
        }
        public static bool SignupUser(Credential cre)
        {
            bool signup;
            User user = UserPersistence.GetUser(cre.UserId);
            if (user != null)
            {
                return false;
            }
            string salt = EncryptionManager.PasswordSalt;
            string passHash = EncryptionManager.EncodePassword(cre.Password1, salt);
            user = new User(cre.UserId, cre.Name, cre.Email, salt, passHash, cre.IsAdmin, "A");
            signup = UserPersistence.InsertUser(user);
            return signup;
        }
        public static void LogoutUSer(HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;
            
        }
    }
}