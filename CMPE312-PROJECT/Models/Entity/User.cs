using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class User
    {
        public String UserID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Salt { get; set; }
        public String PasswordHash { get; set; }
        public decimal IsAdmin { get; set; }
        public String Status { get; set; }

        public User()
        {

        }
        public User(String userID_, String name_, String email_, String salt_, String passwordHash_, int isAdmin_, String status_)
        {
            UserID = userID_;
            Name = name_;
            Email = email_;
            Salt = salt_;
            PasswordHash = passwordHash_;
            IsAdmin = isAdmin_;
            Status = status_;        
        }
    }
}