using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class President
    {
        public decimal ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public decimal TeamID { get; set; }
        public string TeamName { get; set; }

        public President()
        {

        }

        public President(decimal teamID1)
        {
            TeamID = teamID1;
        }
    }
}