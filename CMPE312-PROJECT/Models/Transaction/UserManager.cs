using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;


namespace CMPE312_PROJECT.Models.Transaction
{
    /*
    * This class is created for providing connection between model and controller about User in some cases.
    */
    public class UserManager
    {
        public static User user;

        /*
        * This method takes a Credential object as parameter and checks the database if that credential can be authenticated or not.
        * If this operation succeeds, the method returns true.
        */
        public static bool AuthenticateUser(Credential cre, HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;
            session["IsPresident"] = 0;

            user = UserManager.getUserById(cre.UserId);
            if(user == null) { 
            return false;
            }
            if(user.Status.Equals("I"))
            {
                return false;
            }
            string passHash = EncryptionManager.EncodePassword(cre.Password1, user.Salt);
            if(passHash != user.PasswordHash)
            {
                return false;
            }

            /*else if (user.Status.Equals("I"))
            {
                return false;
            }*/

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

        /*
        * This method takes a Credential object as parameter and adds this credential object to the database by converting it into a user object. 
        * If this operation succeeds, the method returns true.
        */
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

        /*
        * This method takes a Credential object and a User object as parameter and updates that user with credential data. 
        * If this operation succeeds, the method returns true.
        */
        public static bool ChangeUserPassword(User user, Credential cre)
        {
            string salt = EncryptionManager.PasswordSalt;
            string passHash = EncryptionManager.EncodePassword(cre.Password1, salt);
            user.Salt = salt;
            user.PasswordHash = passHash;
            bool updated = UserManager.UpdateUser(user);
            return updated;
        }

        /*
        * This method takes a HttpSessionStateBase object as parameter and logs out current user from the application. 
        */
        public static void LogoutUSer(HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;
            session["IsPresident"] = 0;

        }

        /*
        * This method takes a Credential object and HttpSessionStateBase object as parameter and checks this user object if it is exist in the database by its ID. 
        * If it is exist, the method changes the data of that user with given data by using UpdateUser() method of UserPersistance class. 
        * If this operation succeeds, the method returns true.
        */
        public static bool UpdateUser (Credential cre, HttpSessionStateBase session)
        {
            user = UserPersistence.GetUser(cre.UserId);
            if (user == null)
            {
                return false;
            }

            string salt = EncryptionManager.PasswordSalt;
            string passHash = EncryptionManager.EncodePassword(cre.Password1, salt);

            user = new User(cre.UserId, cre.Name, cre.Email, salt, passHash, cre.IsAdmin, cre.IsPresident, "A");

            bool done = UserPersistence.UpdateUser(user);
            return done;

        }

        /*
        * This method takes a String object as parameter and returns a user if user, whose ID was given by that String object, is exist in the database by using GetUser() method of UserPersistance class.
        */
        public static User getUserById(string ID)
        {
            User user = UserPersistence.GetUser(ID);
            if(user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }

        /*
        * This method takes a User object as parameter and updates that user's data by using UpdateUser() method of UserPersistance class. 
        * If this operation succeeds, the method returns true.
        */
        public static bool UpdateUser(User user)
        {
            bool updated = UserPersistence.UpdateUser(user);
            return updated;

        }
    }
}