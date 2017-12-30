using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    /*
    * This class is created for storing User data in the application after getting it from the database.
    */
    public class User
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
        public decimal IsAdmin { get; set; }
        public decimal PresidentID { get; set; }
        public string Status { get; set; }

        public User()
        {

        }
        public User(String userID_, String name_, String email_, String salt_, String passwordHash_, decimal isAdmin_, decimal presidentID_, String status_)
        {
            UserID = userID_;
            Name = name_;
            Email = email_;
            Salt = salt_;
            PasswordHash = passwordHash_;
            IsAdmin = isAdmin_;
            PresidentID = presidentID_;
            Status = status_;        
        }
    }
}