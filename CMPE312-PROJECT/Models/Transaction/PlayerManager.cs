using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;

namespace CMPE312_PROJECT.Models.Transaction
{
    public class PlayerManager
    {
        public static bool CheckPlayer (Player player1)
        {
            Player p = PlayerPersistence.GetPlayer(player1);

            if (p == null)
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