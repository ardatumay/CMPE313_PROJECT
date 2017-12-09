using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Credential
    {
        public Credential()
        {

        }

        public string UserId { get; set; }
        public string Password { get; set; }
    }
}