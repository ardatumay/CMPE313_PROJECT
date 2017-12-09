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
        public int isAdmin { get; set; }
        public String status { get; set; }

        public User()
        {

        }
    }
}