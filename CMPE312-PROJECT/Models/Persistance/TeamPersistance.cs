﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;

namespace CMPE312_PROJECT.Models.Persistance
{
    /*
    * This class is created for getting, adding, deleting and updating Team data which is stored in the database.
    */
    public class TeamPersistance
    {
        /*
        * This method takes a Team object as parameter and returns a team by its name if the parameter is exist in the database.
        */
        public static Team GetTeam (Team team1)
        {
            string sqlQuery = "select * from TEAM where NAME='" + team1.Name.ToUpper() + "'"; 
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Team.
            object[] dataRow = rows[0];
            //DateTime dateAdded = DateTime.Parse(dataRow[3].ToString());
            Team team = new Team { ID = (decimal)dataRow[0], Name = (String)dataRow[1], City = (String)dataRow[2], Foundation = (decimal)dataRow[3], Budget = (decimal)dataRow[4], NumberOfChampionship = (decimal)dataRow[5], Point = (decimal)dataRow[6]};
            return team;
        }

        /*
        * This method takes a Team object as parameter and returns a team by its ID if the parameter is exist in the database.
        */
        public static Team GetTeamByID(Team team1)
        {
            string sqlQuery = "select * from TEAM where ID='" + team1.ID + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Team.
            object[] dataRow = rows[0];
            //DateTime dateAdded = DateTime.Parse(dataRow[3].ToString());
            Team team = new Team { ID = (decimal)dataRow[0], Name = (String)dataRow[1], City = (String)dataRow[2], Foundation = (decimal)dataRow[3], Budget = (decimal)dataRow[4], NumberOfChampionship = (decimal)dataRow[5], Point = (decimal)dataRow[6] };
            return team;
        }

        /*
        * This method takes a Team object as parameter and checks this team object if it is exist in the database by its ID. If it is exist, the method changes the data of that team with given data. 
        * If this operation succeeds, the method returns true.
        */
        public static bool UpdateTeam(Team team1)
        {

            string sql = "Update TEAM set NAME='" + team1.Name.ToUpper() + "', CITY='" + team1.City.ToUpper() + "', FOUNDATION='" + team1.Foundation + "', BUDGET='" + team1.Budget + "', NUMBER_OF_CHAMPIONSHIP='" + team1.NumberOfChampionship + "' where ID='" + team1.ID + "'";
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
        * This method takes a Team object as parameter and adds this team object to the database. 
        * If this operation succeeds, the method returns true.
        */
        public static bool AddTeam(Team team1)
        {
            int result = -2;
            string sql1 = "Select * from TEAM";
            List<object[]> rows1 = RepositoryManager.Repository.DoQuery(sql1);
            //object[] dataRowCount = rows1[0];
            //decimal IdCount = (decimal)dataRowCount[0];
            if (rows1.Count == 0)
            {
                string sql2 = "Insert into team (ID, NAME, CITY, FOUNDATION, BUDGET, NUMBER_OF_CHAMPIONSHIP, POINTS) values ('" + 1 + "','" + team1.Name.ToUpper() + "','" + team1.City.ToUpper() + "','" + team1.Foundation + "','" + team1.Budget + "','" + team1.NumberOfChampionship + "','" + team1.Point + "')";
                result = RepositoryManager.Repository.DoCommand(sql2);
            }
            else
            {
                decimal MaxId = -1; 
                string sql3 = "Select max(ID) from TEAM";
                List<object[]> rows2 = RepositoryManager.Repository.DoQuery(sql3);
                foreach (object[] dataRow in rows2)
                {
                    //int.TryParse((String)dataRow[0], out MaxId);
                    MaxId = Convert.ToDecimal(dataRow[0]);
                }
                decimal NewId = MaxId + 1; 
                string sql4 = "Insert into team (ID, NAME, CITY, FOUNDATION, BUDGET, NUMBER_OF_CHAMPIONSHIP, POINTS) values ('" + NewId + "','" + team1.Name.ToUpper() + "','" + team1.City.ToUpper() + "','" + team1.Foundation + "','" + team1.Budget + "','" + team1.NumberOfChampionship + "','" + team1.Point + "')";
                result = RepositoryManager.Repository.DoCommand(sql4);
            }
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
         * This method takes a decimal parameter which is teamID. Then checks the database and returns all of the players of that team.
         */
        public static List<Player> GetTeamPlayers(decimal teamID)
        {
            List<Player> players = new List<Player>();
            string sqlQuery = "select * from PLAYER where TEAM_ID='" + teamID + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {
                Player player = new Player { ID = (decimal)dataRow[0], Name = (String)dataRow[1], Surname = (String)dataRow[2], BirthDate = (String)dataRow[3], Position = (String)dataRow[4], TransferFee = (decimal)dataRow[5], Salary = (decimal)dataRow[6], TeamID = (decimal)dataRow[7] };
                players.Add(player);
            }

            return players;
        }

        /*
         * This method takes a Team object as parameter and checks the database and returns all of the players of that team.
         */
        public static List<Player> GetTeamPlayers(Team team)
        {
            List<Player> players = new List<Player>();
            string sqlQuery = "select * from PLAYER where TEAM_ID='" + team.ID + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {
                Player player = new Player { ID = (decimal)dataRow[0], Name = (String)dataRow[1], Surname = (String)dataRow[2], BirthDate = (String)dataRow[3], Position = (String)dataRow[4], TransferFee = (decimal)dataRow[5], Salary = (decimal)dataRow[6], TeamID = (decimal)dataRow[7] };
                players.Add(player);
            }

            return players;
        }

        /*
         * This method returns all of teams from the database.
         */
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

            // Use the data from the first returned row (should be the only one) to create a Team.
            foreach (object[] dataRow in rows)
            {
                //DateTime dateAdded = DateTime.Parse(dataRow[3].ToString());
                Team team = new Team { ID = (decimal)dataRow[0], Name = (String)dataRow[1], City = (String)dataRow[2], Foundation = (decimal)dataRow[3], Budget = (decimal)dataRow[4], NumberOfChampionship = (decimal)dataRow[5], Point = (decimal)dataRow[6] };
                teams.Add(team); 
            }

            return teams;
        }


        public static List<Team> GetTeamsByPointOrder()
        {
            List<Team> teams = new List<Team>();
            string sqlQuery = "select * from TEAM order by POINTS DESC";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Team.
            foreach (object[] dataRow in rows)
            {
                //DateTime dateAdded = DateTime.Parse(dataRow[3].ToString());
                Team team = new Team { ID = (decimal)dataRow[0], Name = (String)dataRow[1], City = (String)dataRow[2], Foundation = (decimal)dataRow[3], Budget = (decimal)dataRow[4], NumberOfChampionship = (decimal)dataRow[5], Point = (decimal)dataRow[6] };
                teams.Add(team);
            }

            return teams;
        }

        /*
        * This method takes a Team object as parameter and deletes this team object from the database if it is exist. 
        * If this operation succeeds, the method returns true.
        */
        public static bool DeleteTeam(Team team1)
        {
            string sql = "delete from TEAM where NAME='" + team1.Name.ToUpper() + "'";
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
        * This method returns number of teams in the database.
        */
        public static decimal GetNumberOfTeams()
        {
            string sqlQuery = "SELECT COUNT(*) FROM TEAM";
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