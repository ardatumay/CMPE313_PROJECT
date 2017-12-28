using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;
using CMPE312_PROJECT.Models.Persistance;


namespace CMPE312_PROJECT.Models.Transaction
{
    /*
    * This class is created for providing connection between model and controller in some cases.
    */
    public class TeamManager
    
        public static bool AddTeam(Team team_)
        {
            bool isAdded;
            Team team = TeamPersistance.GetTeam(team_);
            if (team != null)
            {
                return false;
            }
            isAdded = TeamPersistance.AddTeam(team_);
            return isAdded;
        }

    }
}