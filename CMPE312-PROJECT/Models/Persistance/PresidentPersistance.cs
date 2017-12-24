using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;

namespace CMPE312_PROJECT.Models.Persistance
{
    public class PresidentPersistance
    {
        public static President GetPresident(President president1)
        {
            string sqlQuery = "select * from PRESIDENT where ID='" + president1.ID + "'"; 
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Book.
            object[] dataRow = rows[0];
            DateTime dateAdded = DateTime.Parse(dataRow[3].ToString());
            President president = new President { ID = (int)dataRow[0], name = (String)dataRow[1], surname = (String)dataRow[2], birthDate = dateAdded, teamID = (int)dataRow[4] };
            return president;
        }

        public static bool AddPresident(President president1)
        {
            System.Diagnostics.Debug.WriteLine("DateTime: " + president1.birthDate.ToString("yyyy-MM-dd"));

            string sql = "insert into PRESIDENT values ('"
                + president1.ID + "', '"
                + president1.name + "', "
                + president1.surname + "', '"
                + president1.birthDate.ToString("yyyy-MM-dd") + "', '"
                + president1.teamID + "')";

            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        public static bool DeletePresident(President president1)
        {
            string sql = "delete from PRESIDENT where ID=" + president1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public static bool UpdatePresident(President president1)
        {

            string sql = "Update PRESIDENT set NAME='" + president1.name + "', SURNAME='" + president1.surname + "', BIRTH_DATE='" + president1.birthDate + "', TEAM_ID='" + president1.teamID + "' where ID=" + president1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}