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
            President president = new President { ID = (int)dataRow[0], Name = (String)dataRow[1], Surname = (String)dataRow[2], BirthDate = (String)dataRow[3], TeamID = (int)dataRow[4] };
            return president;
        }

        public static bool AddPresident(President president1)
        {
            string sql1 = "SELECT * FROM PRESIDENT WHERE ID = (SELECT MAX(ID) from PRESIDENT); ";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sql1);

            if (rows.Count == 0)
            {
                president1.ID = 1;
            }
            else
            {
                object[] dataRow = rows[0];
                president1.ID = Convert.ToDecimal(dataRow[0]) + 1;
            }

            string sql = "insert into PRESIDENT values ('"
                + president1.ID + "', '"
                + president1.Name.ToUpper() + "', '"
                + president1.Surname.ToUpper() + "', '"
                + president1.BirthDate + "', '"
                + president1.TeamID + "')";

            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        public static bool DeletePresident(President president1)
        {
            string sql = "delete from PRESIDENT where TEAM_ID=" + president1.TeamID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public static bool UpdatePresident(President president1)
        {

            string sql = "Update PRESIDENT set NAME='" + president1.Name.ToUpper() + "', SURNAME='" + president1.Surname.ToUpper() + "', BIRTH_DATE='" + president1.BirthDate + "', TEAM_ID='" + president1.TeamID + "' where ID=" + president1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public static decimal GetNumberOfPresident()
        {
            string sqlQuery = "SELECT COUNT(*) FROM PRESIDENT";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return -1;
            }

            // Use the data from the first returned row (should be the only one) to create a Team.
            object[] dataRow = rows[0];

            decimal number = Convert.ToDecimal(dataRow[0]);

            return number;
        }
    }
}