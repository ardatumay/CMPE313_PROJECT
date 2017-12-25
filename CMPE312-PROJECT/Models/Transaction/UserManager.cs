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
        public static User user;
        public static bool AuthenticateUser(Credential cre, HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;
            session["IsPresident"] = 0;

            user = UserPersistence.GetUser(cre.UserId);
            if(user == null) { 
            return false;
            }
            string passHash = EncryptionManager.EncodePassword(cre.Password1, user.Salt);
            if(passHash != user.PasswordHash)
            {
                return false;
            }

            else if (user.Status.Equals("I"))
            {
                return false;
            }

            else
            {
                session["LoggedIn"] = true;

                if (user.IsAdmin == 1)
                {
                    session["IsAdmin"] = true;
                }

                if (user.IsAdmin == 0)
                {
                    session["IsAdmin"] = false;
                }
                
                if (user.PresidentID  != 0)
                {
                    session["IsPresident"] = user.PresidentID;
                }

                if (user.PresidentID == 0)
                {
                    session["IsPresident"] = 0;
                    decimal a = Convert.ToDecimal(session["IsPresident"]);
                }

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
            user = new User(cre.UserId, cre.Name, cre.Email, salt, passHash, cre.IsAdmin, cre.IsPresident, "A");
            signup = UserPersistence.InsertUser(user);
            return signup;
        }
        public static void LogoutUSer(HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;
            session["IsPresident"] = 0;

        }

        public static bool UpdateUser (Credential cre, HttpSessionStateBase session)
        {
            user = UserPersistence.GetUser(cre.UserId);
            if (user == null)
            {
                return false;
            }

            string salt = EncryptionManager.PasswordSalt;
            string passHash = EncryptionManager.EncodePassword(cre.Password1, salt);

            user = new User(cre.UserId, cre.Name, cre.Email, salt, passHash, cre.IsAdmin, 0, "A");

            bool done = UserPersistence.UpdateUser(user);
            return done;

        }
    }
}