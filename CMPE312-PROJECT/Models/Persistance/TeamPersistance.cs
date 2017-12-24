using System;
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
            string sqlQuery = "select * from TEAM where NAME='" + team1.Name + "'"; 
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Team.
            object[] dataRow = rows[0];
            //DateTime dateAdded = DateTime.Parse(dataRow[3].ToString());
            Team team = new Team { ID = (decimal)dataRow[0], Name = (String)dataRow[1], City = (String)dataRow[2], Foundation = (decimal)dataRow[3], Budget = (decimal)dataRow[4], NumberOfChampionship = (decimal)dataRow[5]};
            return team;
        }

        /*
         *Updates the team information.
         */

        public static bool UpdateTeam(Team team1)
        {

            string sql = "Update TEAM set NAME='" + team1.Name.ToUpper() + "', CITY='" + team1.City.ToUpper() + "', FOUNDATION='" + team1.Foundation + "', BUDGET='" + team1.Budget + "', NUMBER_OF_CHAMPIONSHIP='" + team1.NumberOfChampionship + "' where ID=" + team1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
         * Inserts new team into database.
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
                string sql2 = "Insert into team (ID, NAME, CITY, FOUNDATION, BUDGET, NUMBER_OF_CHAMPIONSHIP) values ('" + 1 + "','" + team1.Name.ToUpper() + "','" + team1.City.ToUpper() + "','" + team1.Foundation + "','" + team1.Budget + "','" + team1.NumberOfChampionship + "')";
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
                string sql4 = "Insert into team (ID, NAME, CITY, FOUNDATION, BUDGET, NUMBER_OF_CHAMPIONSHIP) values ('" + NewId + "','" + team1.Name.ToUpper() + "','" + team1.City.ToUpper() + "','" + team1.Foundation + "','" + team1.Budget + "','" + team1.NumberOfChampionship + "')";
                result = RepositoryManager.Repository.DoCommand(sql4);
            }
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
         * Get all Team data from the database and return an array of Teams.
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

            // Use the data from the first returned row (should be the only one) to create a Team.
            foreach (object[] dataRow in rows)
            {
                //DateTime dateAdded = DateTime.Parse(dataRow[3].ToString());
                Team team = new Team { ID = (decimal)dataRow[0], Name = (String)dataRow[1], City = (String)dataRow[2], Foundation = (decimal)dataRow[3], Budget = (decimal)dataRow[4], NumberOfChampionship = (decimal)dataRow[5] };
                teams.Add(team); 
            }

            return teams;
        }
    }
}