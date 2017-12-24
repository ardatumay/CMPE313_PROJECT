using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class President
    {
        public decimal ID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string birthDate { get; set; }
        public decimal teamID { get; set; }
        public string teamName { get; set; }

        public President()
        {

        }

        public President(decimal teamID1)
        {
            teamID = teamID1;
        }
    }
}