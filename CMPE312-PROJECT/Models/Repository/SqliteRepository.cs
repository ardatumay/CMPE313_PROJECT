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
        private string databaseFile = "C:\\Users\\Batuhan\\cmpe312-project.sqlite";


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
                string TeamTable = "CREATE TABLE TEAM ( ID DECIMAL, NAME VARCHAR(50) UNIQUE, CITY VARCHAR(50), FOUNDATION DECIMAL, BUDGET DECIMAL, NUMBER_OF_CHAMPIONSHIP DECIMAL, POINTS DECIMAL, PRIMARY KEY(ID))";
                string PlayerTable = "CREATE TABLE PLAYER (ID DECIMAL, NAME VARCHAR(50), SURNAME VARCHAR(50), BIRTH_DATE VARCHAR(50), POSITION VARCHAR(50), TRANSFER_FEE DECIMAL, SALARY DECIMAL, TEAM_ID DECIMAL, PRIMARY KEY(ID), FOREIGN KEY (TEAM_ID) REFERENCES TEAM(ID), FOREIGN KEY (POSITION) REFERENCES POSITIONS(POSITION))";
                string CoachTable = "CREATE TABLE COACH ( ID DECIMAL, NAME VARCHAR(50), SURNAME VARCHAR(50), BIRTH_DATE VARCHAR(50), SALARY DECIMAL, TEAM_ID DECIMAL, PRIMARY KEY(ID), FOREIGN KEY (TEAM_ID) REFERENCES TEAM(ID))";
                string PresidentTable = "CREATE TABLE PRESIDENT ( ID DECIMAL, NAME VARCHAR(50), SURNAME VARCHAR(50), BIRTH_DATE VARCHAR(50), TEAM_ID DECIMAL, PRIMARY KEY(ID), FOREIGN KEY (TEAM_ID) REFERENCES TEAM(ID))";
                string UserTable = "CREATE TABLE USER (USER_ID VARCHAR(50), NAME VARCHAR(50), EMAIL VARCHAR(50), SALT VARCHAR(50), HASHEDPASSWORD VARCHAR(50), IS_ADMIN DECIMAL, STATUS VARCHAR(1), PRIMARY KEY (USER_ID))";
                string PositionsTable = "CREATE TABLE POSITIONS (POSITION VARCHAR(50), PRIMARY KEY (POSITION))";
                DoCommand(TeamTable);
                DoCommand(PlayerTable);
                DoCommand(CoachTable);
                DoCommand(PresidentTable);
                DoCommand(UserTable);
                DoCommand(PositionsTable);

                DefaultData();
            }

            return success;
        }

        public void DefaultData ()
        {
            string AddingPositions = "INSERT INTO POSITIONS VALUES ('GK'), ('LB'), ('CB'), ('RB'), ('CDM'), ('LM'), ('CM'), ('RM'), ('CAM'), ('ST');";
            DoCommand(AddingPositions);

            UserManager.SignupUser(new Credential("admin", "123", "email", "admin", 1));

            string DefaultTeams = "INSERT INTO TEAM VALUES ('1', 'BEÞÝKTAÞ', 'ISTANBUL', '1903', '19', '15', '30')," +
                "('2', 'GALATASARAY', 'ISTANBUL', '1905', '18', '20', '32')," +
                "('3', 'FENERHABÇE', 'ISTANBUL', '1907', '17', '19', '33')," +
                "('4', 'BAÞAKÞEHÝR', 'ISTANBUL', '1990', '16', '0', '36')," +
                "('5', 'TRABZONSPOR', 'TRABZON', '1967', '15', '6', '29')," +
                "('6', 'ANTALYASPOR', 'ANTALYA', '1966', '14', '0', '17')," +
                "('7', 'OSMANLISPOR', 'ANKARA', '1978', '13', '0', '14')," +
                "('8', 'BURSASPOR', 'BURSA', '1963', '12', '1', '25')," +
                "('9', 'KAYSERÝSPOR', 'KAYSERÝ', '1966', '11', '0', '30')," +
                "('10', 'GÖZTEPE', 'ÝZMÝR', '1925', '10', '0', '30')," +
                "('11', 'KONYASPOR', 'KONYA', '1981', '9', '0', '15')," +
                "('12', 'ALANYASPOR', 'ANTALYA', '1948', '8', '0', '18')," +
                "('13', 'GENÇLERBÝRLÝÐÝ', 'ANKARA', '1923', '7', '0', '14')," +
                "('14', 'YENÝ MALATYASPOR', 'MALATYA', '1986', '6', '0', '22')," +
                "('15', 'AKHÝSARSPOR', 'MANÝSA', '1970', '5', '0', '19')," +
                "('16', 'KASIMPAÞA', 'ISTANBUL', '1921', '4', '0', '19')," +
                "('17', 'SÝVASSPOR', 'SÝVAS', '1967', '3', '0', '26')," +
                "('18', 'KARABÜKSPOR', 'ZONGULDAK', '1969', '2', '0', '9');";

            string DefaultCoaches = "INSERT INTO COACH VALUES ('1', 'ÞENOL', 'GÜNEÞ', '01/06/1952', '4.8', '1')," +
                "('2', 'FATÝH', 'TERÝM', '04/09/1953', '6', '2')," +
                "('3', 'AYKUT', 'KOCAMAN', '05/04/1965', '4', '3')," +
                "('4', 'ABDULLAH', 'AVCI', '31/07/1963', '3.5', '4')," +
                "('5', 'RIZA', 'ÇALIMBAY', '02/02/1963', '3', '5')," +
                "('6', 'LEONARDO', 'ARAUJO', '05/09/1969', '3', '6')," +
                "('7', 'ÝRFAN', 'BUZ', '15/04/1967', '2', '7')," +
                "('8', 'PAUL', 'LE GUEN', '01/03/1964', '2.5', '8')," +
                "('9', 'MARIUS', 'SUMUDÝCA', '04/03/1971', '2.5', '9')," +
                "('10', 'TAMER', 'TUNA', '01/07/1976', '2', '10')," +
                "('11', 'MUSTAFA REÞÝT', 'AKÇAY', '12/12/1958', '2', '11')," +
                "('12', 'SAFET', 'SUSÝC', '13/04/1955', '2', '12')," +
                "('13', 'ÜMÝT', 'ÖZAT', '30/10/1976', '1.5', '13')," +
                "('14', 'EROL', 'BULUT', '30/01/1975', '1.5', '14')," +
                "('15', 'OKAN', 'BURUK', '19/10/1973', '2', '15')," +
                "('16', 'KEMAL', 'ÖZDEÞ', '10/05/1970', '1.5', '16')," +
                "('17', 'SAMET', 'AYBABA', '05/09/1956', '1.5', '17')," +
                "('18', 'TONY', 'POPOVÝC', '04/07/1973', '1.5', '18');";

            string DefaultPlayers1 = "INSERT INTO PLAYER VALUES ('1', 'ATIBA', 'HUTCHINSON', '08/02/1983', 'CDM', '1.5', '1.5', '1')," +
                "('2', 'FABRICIO', 'RAMIREZ', '31/12/1987', 'GK', '2.75', '1.5', '1')," +
                "('3', 'OÐUZHAN', 'ÖZYAKUP', '23/09/1992', 'CM', '12', '1.5', '1')," +
                "('4', 'FERNANDO', 'MUSLERA', '16/06/1986', 'GK', '11', '4', '2')," +
                "('5', 'BAFETÝMBÝ', 'GOMÝS', '06/08/1985', 'ST', '4', '4', '2')," +
                "('6', 'YASÝN', 'ÖZTEKÝN', '19/03/1987', 'LM', '3.5', '4', '2')," +
                "('7', 'MARTIN', 'SKRTEL', '15/12/1984', 'CB', '4.5', '3', '3')," +
                "('8', 'ÝSMAÝL', 'KÖYBAÞI', '10/07/1989', 'LB', '3', '3', '3')," +
                "('9', 'OZAN', 'TUFAN', '23/03/1995', 'CM', '5', '3', '3');";

            DoCommand(DefaultTeams);
            DoCommand(DefaultCoaches);
            DoCommand(DefaultPlayers1);
        }
    }
}
