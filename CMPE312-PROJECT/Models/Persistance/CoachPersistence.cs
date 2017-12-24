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
            string sqlQuery = "select * from COACH where NAME='" + coach1.name.ToUpper() + "'"; 
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Book.
            object[] dataRow = rows[0];
            //DateTime dateAdded = DateTime.Parse(dataRow[3].ToString());
            Coach coach = new Coach { ID = (decimal)dataRow[0], name = (String)dataRow[1], surname = (String)dataRow[2], birthDate = (String)dataRow[3], salary = (decimal)dataRow[4], teamID = (decimal)dataRow[5] };
            return coach;
        }

        public static bool AddCoach(Coach coach1)
        {
           /* System.Diagnostics.Debug.WriteLine("DateTime: " + coach1.birthDate.ToString("yyyy-MM-dd"));

            string sql = "insert into COACH values ('"
                + coach1.ID + "', '"
                + coach1.name + "', "
                + coach1.surname + "', '"
                + coach1.birthDate + "', '"
                + coach1.salary + "', '"
                + coach1.teamID + "')";
                
            RepositoryManager.Repository.DoCommand(sql);
            return true;*/

            int result = -2;
            string sql1 = "Select * from COACH";
            List<object[]> rows1 = RepositoryManager.Repository.DoQuery(sql1);
            //object[] dataRowCount = rows1[0];
            //decimal IdCount = (decimal)dataRowCount[0];
            if (rows1.Count == 0)
            {
                string sql2 = "Insert into COACH (ID, NAME, SURNAME, BIRTH_DATE, SALARY, TEAM_ID) values ('" + 1 + "','" + coach1.name.ToUpper() + "','" + coach1.surname.ToUpper() + "','" + coach1.birthDate + "','" + coach1.salary + "','" + coach1 .teamID + "')";
                result = RepositoryManager.Repository.DoCommand(sql2);
            }
            else
            {
                decimal MaxId = -1;
                string sql3 = "Select max(ID) from COACH";
                List<object[]> rows2 = RepositoryManager.Repository.DoQuery(sql3);
                foreach (object[] dataRow in rows2)
                {
                    //int.TryParse((String)dataRow[0], out MaxId);
                    MaxId = Convert.ToDecimal(dataRow[0]);
                }
                decimal NewId = MaxId + 1;
                string sql4 = "Insert into COACH (ID, NAME, SURNAME, BIRTH_DATE, SALARY, TEAM_ID) values ('" + NewId + "','" + coach1.name.ToUpper() + "','" + coach1.surname.ToUpper() + "','" + coach1.birthDate + "','" + coach1.salary + "','" + coach1.teamID + "')";
                result = RepositoryManager.Repository.DoCommand(sql4);
            }
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public static bool DeleteCoach(Coach coach1)
        {
            string sql = "delete from COACH where ID='" + coach1.ID + "'";
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public static bool UpdateCoach(Coach coach1)
        {

            string sql = "Update COACH set NAME='" + coach1.name.ToUpper() + "', SURNAME='" + coach1.surname.ToUpper() + "', BIRTH_DATE='" + coach1.birthDate + "', SALARY='" + coach1.salary + "', TEAM_ID='" + coach1.teamID + "' where ID=" + coach1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}