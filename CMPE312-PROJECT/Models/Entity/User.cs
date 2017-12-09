using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class User
    {
        String userid { get; set; }
        String password { get; set; }
        String name { get; set; }
        String salt { get; set; }
        String passwordHash { get; set; }
        bool isAdmin { get; set; }
    }
}