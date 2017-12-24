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
    public class PresidentManager
    {
        public static bool CheckPresident(President president1)
        {
            President president = PresidentPersistance.GetPresident(president1);

            if (president == null)
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