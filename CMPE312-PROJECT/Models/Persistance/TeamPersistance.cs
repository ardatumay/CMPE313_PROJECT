﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;

namespace CMPE312_PROJECT.Models.Persistance
{
    public class TeamPersistance
    {

        public static Team GetTeam (Team team1)
        {
            string sqlQuery = "select * from TEAM where NAME='" + team1.name + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Book.
            object[] dataRow = rows[0];
            Team team = new Team { ID = (decimal)dataRow[0], name = (String)dataRow[1], city = (String)dataRow[2], foundation = (decimal)dataRow[3], budget = (decimal)dataRow[4], numberOfChampionship = (decimal)dataRow[5]};
            return team;
        }

        public static bool UpdateTeam(Team team1)
        {

            string sql = "Update TEAM set NAME='" + team1.name + "', CITY='" + team1.city + "', FOUNDATION='" + team1.foundation + "', BUDGET='" + team1.budget + "', NUMBER_OF_CHAMPIONSHIP='" + team1.numberOfChampionship + "' where ID=" + team1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
         * Get all Book data from the database and return an array of Books.
         */
        public static List<Player> GetTeamPlayers(int teamID)
        {
            List<Player> players = new List<Player>();

            string sqlQuery = "select * from PLAYER where TEAM_ID='" + teamID + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {
                Player player = new Player { ID = (decimal)dataRow[0], name = (String)dataRow[1], surname = (String)dataRow[2], birthDate = (String)dataRow[3], position = (String)dataRow[4], transferFee = (decimal)dataRow[5], salary = (decimal)dataRow[6], teamID = (decimal)dataRow[7] };
                players.Add(player);
            }

            return players;
        }

        public static List<Team> GetTeams ()
        {
            List<Team> teams = new List<Team>();
            string sqlQuery = "select * from TEAM";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Book.
            foreach (object[] dataRow in rows)
            {
                DateTime dateAdded = DateTime.Parse(dataRow[3].ToString());
                Team team = new Team { ID = (int)dataRow[0], name = (String)dataRow[1], city = (String)dataRow[2], foundation = (decimal)dataRow[3], budget = (long)dataRow[4], numberOfChampionship = (int)dataRow[5] };
                teams.Add(team); 
            }

            return teams;
        }
    }
}