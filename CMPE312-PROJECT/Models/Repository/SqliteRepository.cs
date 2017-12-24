using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;
using CMPE312_PROJECT.Models.Persistance;

namespace CMPE312_PROJECT.Models.Repository
{
    /*
     * This class creates and accesses an SQLite database.
     */
    public class SqliteRepository : IRepository
    {
        // Location of the database file 
        private string databaseFile = "C:\\Users\\User\\cmpe312-project.sqlite";


        private SQLiteConnection dbConnection;

        public bool IsOpen { get { return isOpen; } }
        private bool isOpen = false;

        /*
         * When the Repository shuts down, it should close the DB if it's open.
         */
        ~SqliteRepository() {
            if (IsOpen) {
                Close();
            }
        }

        /*
         * Open the database. Return true iff the open succeeds, or it was
         * already open.
         */
        public bool Open()
        {
            if (IsOpen) {
                return true;
            }
            dbConnection =
                new SQLiteConnection("Data Source=" + databaseFile + ";Version=3;");
            if (dbConnection == null) { return false; }
            dbConnection.Open();
            isOpen = true;
            return true;
        }

        /*
         * Close the database, if it's open.
         */
        public void Close()
        {
            if (!IsOpen) {
                return;
            }
            isOpen = false;
            dbConnection.Close();
        }

        /*
         * Execute an SQL command. 
         * The return value is the number of rows affected by the command.
         */
        public int DoCommand(string sqlCommand)
        {
            if (!IsOpen) 
            {
                return -1;
            }
            SQLiteCommand command = new SQLiteCommand(sqlCommand, dbConnection);
            int result = command.ExecuteNonQuery();
            return result;
        }

        /*
         * Execute an SQL query. 
         * The return value is a List of object arrays, in which each array 
         * represents one row of data returned.
         */
        public List<object[]> DoQuery(string sqlQuery)
        {
            if (!IsOpen)
            {
                return null;
            }
            List<object[]> rows = new List<object[]>();
            SQLiteCommand command = new SQLiteCommand(sqlQuery, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                object[] row = new object[reader.FieldCount];
                reader.GetValues(row);
                rows.Add(row);
            }
            return rows;
        }

        /*
         * Recreate and reinitialize the database.
         * The return value is true iff the initialization succeeds.
         */
        public bool Initialize()
        {
            bool success = true;

            Close();

            try
            {
                SQLiteConnection.CreateFile(databaseFile);
            }
            catch (IOException e)
            {
                success = false;
            }

            bool openResult = Open();
            if (success & openResult)
            {
                string TeamTable = "CREATE TABLE TEAM ( ID DECIMAL, NAME VARCHAR(50) UNIQUE, CITY VARCHAR(50), FOUNDATION DECIMAL, BUDGET DECIMAL, NUMBER_OF_CHAMPIONSHIP DECIMAL, PRIMARY KEY(ID))";
                string PlayerTable = "CREATE TABLE PLAYER (ID DECIMAL, NAME VARCHAR(50), SURNAME VARCHAR(50), BIRTH_DATE VARCHAR(50), POSITION VARCHAR(50), TRANSFER_FEE DECIMAL, SALARY DECIMAL, TEAM_ID DECIMAL, PRIMARY KEY(ID), FOREIGN KEY (TEAM_ID) REFERENCES TEAM(ID))";
                string CoachTable = "CREATE TABLE COACH ( ID DECIMAL, NAME VARCHAR(50), SURNAME VARCHAR(50), BIRTH_DATE VARCHAR(50), SALARY DECIMAL, TEAM_ID DECIMAL, PRIMARY KEY(ID), FOREIGN KEY (TEAM_ID) REFERENCES TEAM(ID))";
                string PresidentTable = "CREATE TABLE PRESIDENT ( ID DECIMAL, NAME VARCHAR(50), SURNAME VARCHAR(50), BIRTH_DATE DATE, TEAM_ID DECIMAL, PRIMARY KEY(ID), FOREIGN KEY (TEAM_ID) REFERENCES TEAM(ID))";
                string UserTable = "CREATE TABLE USER (USER_ID VARCHAR(50), NAME VARCHAR(50), EMAIL VARCHAR(50), SALT VARCHAR(50), HASHEDPASSWORD VARCHAR(50), IS_ADMIN DECIMAL, STATUS VARCHAR(1), PRIMARY KEY (USER_ID))";
                DoCommand(TeamTable);
                DoCommand(PlayerTable);
                DoCommand(CoachTable);
                DoCommand(PresidentTable);
                DoCommand(UserTable);

                UserManager.SignupUser(new Credential("admin", "123", "email", "admin", 1));
            }

            return success;
        }
    }
}
