using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;

namespace CMPE312_PROJECT.Models.Persistance
{
    /*
    * This class is created for getting, adding, deleting and updating Coach data which is stored in the database.
    */
    public class CoachPersistence
    {
        /*
        * This method takes a Coach object as parameter and returns a coach if the parameter is exist in the database.
        */
        public static Coach GetCoach(Coach coach1)
        {
            string sqlQuery = "select * from COACH where NAME='" + coach1.Name.ToUpper() + "'"; 
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Book.
            object[] dataRow = rows[0];
            //DateTime dateAdded = DateTime.Parse(dataRow[3].ToString());
            Coach coach = new Coach { ID = (decimal)dataRow[0], Name = (String)dataRow[1], Surname = (String)dataRow[2], BirthDate = (String)dataRow[3], Salary = (decimal)dataRow[4], TeamID = (decimal)dataRow[5] };
            return coach;
        }

        /*
        * This method takes a Coach object as parameter and adds this coach object to the database. 
        * If this operation succeeds, the method returns true.
        */
        public static bool AddCoach(Coach coach1)
        {
            int result = -2;
            string sql1 = "Select * from COACH";
            List<object[]> rows1 = RepositoryManager.Repository.DoQuery(sql1);
            //object[] dataRowCount = rows1[0];
            //decimal IdCount = (decimal)dataRowCount[0];
            if (rows1.Count == 0)
            {
                string sql2 = "Insert into COACH (ID, NAME, SURNAME, BIRTH_DATE, SALARY, TEAM_ID) values ('" + 1 + "','" + coach1.Name.ToUpper() + "','" + coach1.Surname.ToUpper() + "','" + coach1.BirthDate + "','" + coach1.Salary + "','" + coach1 .TeamID + "')";
                result = RepositoryManager.Repository.DoCommand(sql2);
            }
            else
            {
                /*decimal MaxId = -1;
                string sql3 = "Select max(ID) from COACH";
                List<object[]> rows2 = RepositoryManager.Repository.DoQuery(sql3);
                foreach (object[] dataRow in rows2)
                {
                    //int.TryParse((String)dataRow[0], out MaxId);
                    MaxId = Convert.ToDecimal(dataRow[0]);
                }
                decimal NewId = MaxId + 1;
                string sql4 = "Insert into COACH (ID, NAME, SURNAME, BIRTH_DATE, SALARY, TEAM_ID) values ('" + NewId + "','" + coach1.Name.ToUpper() + "','" + coach1.Surname.ToUpper() + "','" + coach1.BirthDate + "','" + coach1.Salary + "','" + coach1.TeamID + "')";
                result = RepositoryManager.Repository.DoCommand(sql4);*/
                UpdateCoach(coach1);
            }
            //if (result == 1)
            //{
                return true;
            //}
            //return false;
        }

        /*
        * This method takes a Coach object as parameter and deletes this coach object from the database if it is exist. 
        * If this operation succeeds, the method returns true.
        */
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

        /*
        * This method takes a Coach object as parameter and checks this coach object if it is exist in the database by its ID. If it is exist, the method changes the data of that coach with given data. 
        * If this operation succeeds, the method returns true.
        */
        public static bool UpdateCoach(Coach coach1)
        {

            string sql = "Update COACH set NAME='" + coach1.Name.ToUpper() + "', SURNAME='" + coach1.Surname.ToUpper() + "', BIRTH_DATE='" + coach1.BirthDate + "', SALARY='" + coach1.Salary + "', TEAM_ID='" + coach1.TeamID + "' where ID=" + coach1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
        * This method returns number of coaches in the database.
        */
        public static decimal GetNumberOfCoaches()
        {
            string sqlQuery = "SELECT COUNT(*) FROM COACH";
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