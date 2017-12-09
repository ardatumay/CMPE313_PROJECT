using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class User
    {
        public String UserId { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Salt { get; set; }
        public String PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public User (String userid1, String name1, String salt1, String passwordHash1, bool isAdmin1)
        {
            UserId = userid1;
            Name = name1;
            Salt = salt1;
            PasswordHash = passwordHash1;
            IsAdmin = isAdmin1;
        }
    }
}