using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;
using CMPE312_PROJECT.Models.Persistance;


namespace CMPE312_PROJECT.Models.Persistance
{
    public class CommentPersistence
    {
        public static bool addComment(Comment comment)
        {
            int result = -2;
            string sql1 = "Select * from COMMENT";
            List<object[]> rows1 = RepositoryManager.Repository.DoQuery(sql1);
            //object[] dataRowCount = rows1[0];
            //decimal IdCount = (decimal)dataRowCount[0];
            if (rows1.Count == 0)
            {
                string sql2 = "Insert into COMMENT (ID, COMMENT, PLAYER_ID, COACH_ID, PRESIDENT_ID, TEAM_ID) values ('" + 1 + "','" + comment.CommentValue + "','" + 0 + "','" + 0 + "','" + 0 + "','" + comment.TeamID + "')";
                result = RepositoryManager.Repository.DoCommand(sql2);
            }
            else
            {
                decimal MaxId = -1;
                string sql3 = "Select max(ID) from COMMENT";
                List<object[]> rows2 = RepositoryManager.Repository.DoQuery(sql3);
                foreach (object[] dataRow in rows2)
                {
                    //int.TryParse((String)dataRow[0], out MaxId);
                    MaxId = Convert.ToDecimal(dataRow[0]);
                }
                decimal NewId = MaxId + 1;
                string sql4 = "Insert into COMMENT (ID, COMMENT, PLAYER_ID, COACH_ID, PRESIDENT_ID, TEAM_ID) values ('" + NewId + "','" + comment.CommentValue + "','" + 0 + "','" + 0 + "','" + 0 + "','" + comment.TeamID + "')";
                result = RepositoryManager.Repository.DoCommand(sql4);
            }
            if (result == 1)
            {
                return true;
            }
            return false;
        }


    }
}