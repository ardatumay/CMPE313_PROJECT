using System;
using System.Collections.Generic;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;

namespace CMPE312_PROJECT.Models.Repository
{
    /*
     * This class manages CRUD (create, retrieve, update, delete) operations
     * for books.
     */
    public class PlayerPersistence
    {
        /*
         * Retrieve from the database the book matching the ISBN field of
         * the parameter.
         * Return null if the book can't be found.
         */
        public static Player GetPlayer(Player player1)
        {
            string sqlQuery = "select * from PLAYER where NAME='" + player1.name + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Player.
            object[] dataRow = rows[0];
            Player player = new Player { ID = (decimal)dataRow[0], name = (String)dataRow[1], surname = (String)dataRow[2], birthDate = (String)dataRow[3], position = (String)dataRow[4], transferFee = (decimal)dataRow[5], salary = (decimal)dataRow[6], teamID = (decimal)dataRow[7] };
            return player;
        }

        /*
         * Add a Player to the database.
         * Return true iff the add succeeds.
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
                + player1.name + "', '"
                + player1.surname + "', '"
                + player1.birthDate + "', '"
                + player1.position + "', '"
                + player1.transferFee + "', '"
                + player1.salary + "', '"
                + player1.teamID + "')";

            RepositoryManager.Repository.DoCommand(sql);
            return true;
         }

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
         * Update a book that is in the database, replacing all field values except
         * the key field.
         * Return false if the book is not found, based on key field match.
         */
         public static bool UpdatePlayer(Player player1)
         {
          
            string sql = "Update PLAYER set NAME='" +  player1.name + "', SURNAME='" + player1.surname + "', BIRTH_DATE='" + player1.birthDate + "', POSITION='" + player1.position + "', TRANSFER_FEE='" + player1.transferFee + "', SALARY='" + player1.salary + "', TEAM_ID='" + player1.teamID + "' where ID=" + player1.ID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
         }
    }
}