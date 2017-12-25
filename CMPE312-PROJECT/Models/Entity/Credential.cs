using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Credential
    {

        public string UserId { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string OldPassword { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public decimal IsAdmin { get; set; }
        public decimal IsPresident { get; set; }

        public Credential()
        {

        }

        public Credential(string UserId1, string Password, string Email1, string Name1, int IsAdmin1, int IsPresident1) 
        {
            UserId = UserId1;
            Password1 = Password;
            Password2 = Password;
            Email = Email1;
            Name = Name1;
            IsAdmin = IsAdmin1;
            IsPresident = IsPresident1;
        }

    }
}