using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Player
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime birthDate { get; set; }
        public string position { get; set; }
        public long transferFee { get; set; }
        public long salary { get; set; }
        public int teamID { get; set; }

        public Player()
        {
        }
    }
}