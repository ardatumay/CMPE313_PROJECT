using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;

namespace CMPE312_PROJECT.Models.Persistance
{
    public class CoachPersistence
    {
        public static Coach GetCoach(Coach coach1)
        {
            string sqlQuery = "select * from COACH where ID=" + coach1.ID;
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Book.
            object[] dataRow = rows[0];
            Coach coach = new Coach { ID = (int)dataRow[0], name = (String)dataRow[1], surname = (String)dataRow[2], birthDate = (String)dataRow[3], salary = (long)dataRow[4], teamID = (int)dataRow[5] };
            return coach;
        }

        public static bool AddCoach(Coach coach1)
        {
            string sql = "insert into COACH values ('"
                + coach1.ID + "', '"
                + coach1.name + "', "
                + coach1.surname + "', '"
                + coach1.birthDate + "', '"
                + coach1.salary + "', '"
                + coach1.teamID + "')";
                
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        public static bool DeleteCoach(Coach coach1)
        {
            string sql = "delete from COACH where ID=" + coach1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public static bool UpdateCoach(Coach coach1)
        {

            string sql = "Update COACH set NAME='" + coach1.name + "', SURNAME='" + coach1.surname + "', BIRTH_DATE='" + coach1.birthDate + "', SALARY='" + coach1.salary + "', TEAM_ID='" + coach1.teamID + "' where ID=" + coach1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}