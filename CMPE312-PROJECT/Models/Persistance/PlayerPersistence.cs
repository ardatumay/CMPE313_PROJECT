using System;
using System.Collections.Generic;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;

namespace CMPE312_PROJECT.Models.Repository
{
    /*
    * This class is created for getting, adding, deleting and updating Player data which is stored in the database.
    */
    public class PlayerPersistence
    {
        /*
        * This method takes a Player object as parameter and returns a player if the parameter is exist in the database.
        */
        public static Player GetPlayer(Player player1)
        {
            string sqlQuery = "select * from PLAYER where NAME='" + player1.Name.ToUpper() + "' AND SURNAME='" + player1.Surname.ToUpper() + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Player.
            object[] dataRow = rows[0];
            Player player = new Player { ID = (decimal)dataRow[0], Name = (String)dataRow[1], Surname = (String)dataRow[2], BirthDate = (String)dataRow[3], Position = (String)dataRow[4], TransferFee = (decimal)dataRow[5], Salary = (decimal)dataRow[6], TeamID = (decimal)dataRow[7] };
            return player;
        }

        /*
        * This method takes a String object as parameter and returns a player with that parameter ID if the parameter is exist in the database.
        */
        public static Player GetPlayerByID(Player player1)
        {
            string sqlQuery = "select * from PLAYER where ID='" + player1.ID + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Player.
            object[] dataRow = rows[0];
            Player player = new Player { ID = (decimal)dataRow[0], Name = (String)dataRow[1], Surname = (String)dataRow[2], BirthDate = (String)dataRow[3], Position = (String)dataRow[4], TransferFee = (decimal)dataRow[5], Salary = (decimal)dataRow[6], TeamID = (decimal)dataRow[7] };
            return player;
        }



        /*
        * This method takes a Player object as parameter and adds this player object to the database. 
        * If this operation succeeds, the method returns true.
        */
        public static bool AddPlayer(Player player1)
         {
            string sql1 = "SELECT * FROM PLAYER WHERE ID = (SELECT MAX(ID) from PLAYER); ";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sql1);

            if (rows.Count == 0)
            {
                player1.ID = 1;
            }
            else {
            object[] dataRow = rows[0];
            player1.ID = Convert.ToDecimal(dataRow[0])+1;
            }

            string sql = "insert into PLAYER values ('"
                + player1.ID + "', '"
                + player1.Name.ToUpper() + "', '"
                + player1.Surname.ToUpper() + "', '"
                + player1.BirthDate + "', '"
                + player1.Position.ToUpper() + "', '"
                + player1.TransferFee + "', '"
                + player1.Salary + "', '"
                + player1.TeamID + "')";

            RepositoryManager.Repository.DoCommand(sql);
            return true;
         }

        /*
        * This method takes a Player object as parameter and deletes this player object from the database if it is exist. 
        * If this operation succeeds, the method returns true.
        */
        public static bool DeletePlayer(Player player1)
        {
            string sql = "delete from PLAYER where ID=" + player1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
        * This method takes a Player object as parameter and checks this player object if it is exist in the database by its ID. If it is exist, the method changes the data of that player with given data. 
        * If this operation succeeds, the method returns true.
        */
        public static bool UpdatePlayer(Player player1)
         {
          
            string sql = "Update PLAYER set NAME='" +  player1.Name.ToUpper() + "', SURNAME='" + player1.Surname.ToUpper() + "', BIRTH_DATE='" + player1.BirthDate + "', POSITION='" + player1.Position.ToUpper() + "', TRANSFER_FEE='" + player1.TransferFee + "', SALARY='" + player1.Salary + "', TEAM_ID='" + player1.TeamID + "' where ID=" + player1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
         }

        /*
        * This method returns number of positions in the database.
        */
        public static List<Position> GetPositions()
        {
            List<Position> positions = new List<Position>();
            string sqlQuery = "select * from POSITIONS";
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
                Position position = new Position { PositionValue = (string)dataRow[0] };
                positions.Add(position);
            }

            return positions;
        }

        /*
        * This method returns number of players in the database.
        */
        public static decimal GetNumberOfPlayers()
        {
            string sqlQuery = "SELECT COUNT(*) FROM PLAYER";
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