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
    * This class is created for providing connection between model and controller about President in some cases.
    */
    public class PresidentManager
    {
        /*
        * This method takes a President object as parameter and returns a president if the parameter is exist in the database by using GetPresident() method of PresidentPersistance class.
        * If this operation succeeds, the method returns true.
        */
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