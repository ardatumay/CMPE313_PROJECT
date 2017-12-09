using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab7.Models.Entity;
using Lab7.Models.Repository;

namespace Lab7.Models.Transaction
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
            string passHash = EncryptionManager.EncodePassword(cre.Password, user.Salt);
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

      public static void LogoutUSer(HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;
            
        }
    }
}