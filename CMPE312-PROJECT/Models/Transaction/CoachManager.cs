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
    public class CoachManager
    {

        public static bool AddCoach(Coach coach_)
        {
            bool isAdded = true;
            Coach Coach = CoachPersistence.GetCoach(coach_);
            if(Coach != null && Coach.TeamID == coach_.TeamID)
            {
                return false;
            }
            else if (Coach != null)
            {
                isAdded = CoachPersistence.UpdateCoach(coach_);            
            }else if(Coach == null)
            {
                isAdded = CoachPersistence.AddCoach(coach_);
            }
            return isAdded;
        }
    }
}