using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    /*
    * This class is created for storing Player data in the application after getting it from the database.
    */
    public class Player
    {
        public decimal ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public string Position { get; set; }
        public decimal TransferFee { get; set; }
        public decimal Salary { get; set; }
        public decimal TeamID { get; set; }
        public string TeamName { get; set; }

        public Player()
        {

        }
        private Player(decimal PlayerId)
        {
            ID = PlayerId;
        }

        public static Player CreatePlayerById(decimal ID_)
        {
            return new Player(ID_);
        }
    }
}