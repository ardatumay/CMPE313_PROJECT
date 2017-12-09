using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace CMPE312_PROJECT.Models.Repository
{
    /*
     * This class creates and accesses an SQLite database.
     */
    public class SqliteRepository : IRepository
    {
        // Location of the database file 
        private string databaseFile = "C:\\Users\\user\\MyDatabase.sqlite";

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
                string teamTable = "CREATE TABLE TEAM ( ID NUMBER, NAME VARCHAR(50), CITY VARCHAR(50), FOUNDATION DATE, BUDGET NUMBER, NUMBER_OF_CHAMPIONSHIP NUMBER, PRIMARY KEY(ID))";
                string playerTable = "CREATE TABLE PLAYER (ID NUMBER, NAME VARCHAR(50), SURNAME VARCHAR(50), BIRTH_DATE DATE, POSITION VARCHAR(50), TRANSFER_FEE NUMBER, SALARY NUMBER, TEAM_ID NUMBER, PRIMARY KEY(ID), FOREIGN KEY (TEAM_ID) REFERENCES TEAM(ID))";
                string coachTable = "CREATE TABLE COACH ( ID NUMBER, NAME VARCHAR(50), SURNAME VARCHAR(50), BIRTH_DATE DATE, SALARY NUMBER, TEAM_ID NUMBER, PRIMARY KEY(ID), FOREIGN KEY (TEAM_ID) REFERENCES TEAM(ID))";
                string presidentTable = "CREATE TABLE PRESIDENT ( ID NUMBER, NAME VARCHAR(50), SURNAME VARCHAR(50), BIRTH_DATE DATE, TEAM_ID NUMBER, PRIMARY KEY(ID), FOREIGN KEY (TEAM_ID) REFERENCES TEAM(ID))";
                DoCommand(teamTable);
                DoCommand(playerTable);
                DoCommand(coachTable);
                DoCommand(presidentTable);
                /*sql = "insert into book (title, isbn, dateadded) values "
                    + "('Gone With The Wind', 67890123, '2011-01-03')"
                    + ", ('Platos Republic', 80192837, '2013-02-25')"
                    + ", ('Selcuk Altun', 22334455778, '1944-06-15')"
                    + ", ('Die Blechtrommel', 90897856453, '1896-07-06')";
                DoCommand(sql);*/
            }

            return success;
        }
    }
}
