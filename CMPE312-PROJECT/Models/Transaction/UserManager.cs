using System;
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
            string passHash = EncryptionManager.EncodePassword(cre.Password, user.salt);
            if(passHash != user.passwordHash)
            {
                return false;
            }
            else
            {
                session["LoggedIn"] = true;
                session["IsAdmin"] = user.isAdmin;
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