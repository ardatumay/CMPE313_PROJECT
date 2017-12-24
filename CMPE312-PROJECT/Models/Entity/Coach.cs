using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Coach
    {
        public decimal ID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string birthDate { get; set; }
        public decimal salary { get; set; }
        public decimal teamID { get; set; }

        public Coach()
        {

        }
    }
}