﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;
using CMPE312_PROJECT.Models.Persistance;


namespace CMPE312_PROJECT.Models.Persistance
{
    /*
    * This class is created for getting, adding, deleting and updating Comment data which is stored in the database.
    */
    public class CommentPersistence
    {
        /*
        * This method takes a Comment object as parameter and adds that comment for a team to the database. 
        * If this operation succeeds, the method returns true.
        */
        public static bool addCommentTeam(Comment comment)
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

        /*
        * This method takes a Comment object as parameter and adds that comment for a player to the database. 
        * If this operation succeeds, the method returns true.
        */
        public static bool addCommentPlayer(Comment comment)
        {
            int result = -2;
            string sql1 = "Select * from COMMENT";
            List<object[]> rows1 = RepositoryManager.Repository.DoQuery(sql1);
            //object[] dataRowCount = rows1[0];
            //decimal IdCount = (decimal)dataRowCount[0];
            if (rows1.Count == 0)
            {
                string sql2 = "Insert into COMMENT (ID, COMMENT, PLAYER_ID, COACH_ID, PRESIDENT_ID, TEAM_ID) values ('" + 1 + "','" + comment.CommentValue + "','" + comment.PlayerId + "','" + 0 + "','" + 0 + "','" + comment.TeamID + "')";
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
                string sql4 = "Insert into COMMENT (ID, COMMENT, PLAYER_ID, COACH_ID, PRESIDENT_ID, TEAM_ID) values ('" + NewId + "','" + comment.CommentValue + "','" + comment.PlayerId + "','" + 0 + "','" + 0 + "','" + comment.TeamID + "')";
                result = RepositoryManager.Repository.DoCommand(sql4);
            }
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
        * This method takes a Team object as parameter and returns all comments about a team from the database.
        */
        public static List<Comment> GetTeamComments(Team team)
        {
            List<Comment> comments = new List<Comment>();
            string sql1 = "Select * from COMMENT where TEAM_ID='" + team.ID + "'and PLAYER_ID='" + 0 + "'";
            List<object[]> rows1 = RepositoryManager.Repository.DoQuery(sql1);
            if (rows1.Count == 0)
            {
                return null;
            }
            else
            {
                string sql2 = "Select COMMENT from COMMENT where TEAM_ID='" + team.ID + "'and PLAYER_ID='" + 0 + "'";
                List<object[]> rows2 = RepositoryManager.Repository.DoQuery(sql2);
                foreach (object[] dataRow in rows2)
                {
                    Comment comment = new Comment { CommentValue = (string)dataRow[0] };
                    comments.Add(comment);
                }
            }
            return comments;
        }

        /*
        * This method takes a Team object and a Player object as parameter and returns all comments about a player from the database.
        */
        public static List<Comment> GetPlayerComments(Team team, Player player)
        {
            List<Comment> comments = new List<Comment>();
            string sql1 = "Select * from COMMENT where TEAM_ID='" + team.ID + "'and PLAYER_ID='" + player.ID + "'";
            List<object[]> rows1 = RepositoryManager.Repository.DoQuery(sql1);
            if (rows1.Count == 0)
            {
                return null;
            }
            else
            {
                string sql2 = "Select COMMENT from COMMENT where TEAM_ID='" + team.ID + "'and PLAYER_ID='" + player.ID + "'";
                List<object[]> rows2 = RepositoryManager.Repository.DoQuery(sql2);
                foreach (object[] dataRow in rows2)
                {
                    Comment comment = new Comment { CommentValue = (string)dataRow[0] };
                    comments.Add(comment);
                }
            }
            return comments;
        }

        /*
        * This method returns number of comments in the database.
        */
        public static decimal GetNumberOfComments()
        {
            string sqlQuery = "SELECT COUNT(*) FROM COMMENT";
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