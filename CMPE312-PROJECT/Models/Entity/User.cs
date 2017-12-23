using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class User
    {
        public String userID { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public String salt { get; set; }
        public String passwordHash { get; set; }
        public decimal isAdmin { get; set; }
        public String status { get; set; }

        public User()
        {

        }
        public User(String userID_, String name_, String email_, String salt_, String passwordHash_, int isAdmin_, String status_)
        {
            userID = userID_;
            name = name_;
            email = email_;
            salt = salt_;
            passwordHash = passwordHash_;
            isAdmin = isAdmin_;
            status = status_;        
        }
    }
}