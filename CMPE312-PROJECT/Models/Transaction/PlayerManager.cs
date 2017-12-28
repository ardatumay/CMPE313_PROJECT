using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;

namespace CMPE312_PROJECT.Models.Transaction
{
    /*
    * This class is created for providing connection between model and controller about Player in some cases.
    */
    public class PlayerManager
    {
        /*
        * This method takes a Player object as parameter and returns a player if the parameter is exist in the database by using GetPlayer() method of PlayerPersistance class.
        * If this operation succeeds, the method returns true.
        */
        public static bool CheckPlayer (Player player1)
        {
            Player player = PlayerPersistence.GetPlayer(player1);

            if (player == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}