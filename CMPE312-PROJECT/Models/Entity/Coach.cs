using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Coach
    {
        int ID { get; set; }
        string name { get; set; }
        string surname{ get; set; }
        DateTime birthDate { get; set; }
        long salary { get; set; }
        Team team { get; set; }

        public Coach()
        {

        }
    }
}