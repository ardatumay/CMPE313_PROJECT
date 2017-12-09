using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Player
    {
        int ID { get; set; }
        string name { get; set; }
        string surname { get; set; }
        DateTime birthDate { get; set; }
        string position { get; set; }
        long transferFee { get; set; }
        long salary { get; set; }
        Team team { get; set; }

        public Player()
        {
        }
    }
}