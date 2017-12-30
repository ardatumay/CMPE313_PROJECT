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
                string UserTable = "CREATE TABLE USER (USER_ID VARCHAR(50), NAME VARCHAR(50), EMAIL VARCHAR(50), SALT VARCHAR(50), HASHEDPASSWORD VARCHAR(50), IS_ADMIN DECIMAL, PRESIDENT_ID DECIMAL, STATUS VARCHAR(1), PRIMARY KEY (USER_ID), FOREIGN KEY (PRESIDENT_ID) REFERENCES PRESIDENT(ID))";
                string PositionsTable = "CREATE TABLE POSITIONS (POSITION VARCHAR(50), PRIMARY KEY (POSITION))";
                string CommentTable = "CREATE TABLE COMMENT (ID DECIMAL, COMMENT VARCHAR(280), PLAYER_ID DECIMAL, COACH_ID DECIMAL, PRESIDENT_ID DECIMAL, TEAM_ID DECIMAL, PRIMARY KEY(ID), FOREIGN KEY (PLAYER_ID) REFERENCES PLAYER (ID), FOREIGN KEY (COACH_ID) REFERENCES COACH (ID), FOREIGN KEY (PRESIDENT_ID) REFERENCES PRESIDENT (ID), FOREIGN KEY (TEAM_ID) REFERENCES TEAM (ID))";
                DoCommand(TeamTable);
                DoCommand(PlayerTable);
                DoCommand(CoachTable);
                DoCommand(PresidentTable);
                DoCommand(UserTable);
                DoCommand(PositionsTable);
                DoCommand(CommentTable);

                DefaultData();
            }

            return success;
        }

        /*
         * This method inserts default data to the database.
         */
        public void DefaultData ()
        {
            string AddingPositions = "INSERT INTO POSITIONS VALUES ('GK'), ('LB'), ('CB'), ('RB'), ('CDM'), ('LM'), ('CM'), ('RM'), ('CAM'), ('ST');";
            DoCommand(AddingPositions);
        /*
         * Default user information for all of presidents.
         */
            UserManager.SignupUser(new Credential("admin", "adminadmin", "email", "admin", 1, 0));

            string DefaultTeams = "INSERT INTO TEAM VALUES ('1', 'BE��KTA�', 'ISTANBUL', '1903', '19', '15', '30')," +
                "('2', 'GALATASARAY', 'ISTANBUL', '1905', '18', '20', '32')," +
                "('3', 'FENERHAB�E', 'ISTANBUL', '1907', '17', '19', '33')," +
                "('4', 'BA�AK�EH�R', 'ISTANBUL', '1990', '16', '0', '36')," +
                "('5', 'TRABZONSPOR', 'TRABZON', '1967', '15', '6', '29')," +
                "('6', 'ANTALYASPOR', 'ANTALYA', '1966', '14', '0', '17')," +
                "('7', 'OSMANLISPOR', 'ANKARA', '1978', '13', '0', '14')," +
                "('8', 'BURSASPOR', 'BURSA', '1963', '12', '1', '25')," +
                "('9', 'KAYSER�SPOR', 'KAYSER�', '1966', '11', '0', '30')," +
                "('10', 'G�ZTEPE', '�ZM�R', '1925', '10', '0', '30')," +
                "('11', 'KONYASPOR', 'KONYA', '1981', '9', '0', '15')," +
                "('12', 'ALANYASPOR', 'ANTALYA', '1948', '8', '0', '18')," +
                "('13', 'GEN�LERB�RL���', 'ANKARA', '1923', '7', '0', '14')," +
                "('14', 'YEN� MALATYASPOR', 'MALATYA', '1986', '6', '0', '22')," +
                "('15', 'AKH�SARSPOR', 'MAN�SA', '1970', '5', '0', '19')," +
                "('16', 'KASIMPA�A', 'ISTANBUL', '1921', '4', '0', '19')," +
                "('17', 'S�VASSPOR', 'S�VAS', '1967', '3', '0', '26')," +
                "('18', 'KARAB�KSPOR', 'ZONGULDAK', '1969', '2', '0', '9');";

            string DefaultCoaches = "INSERT INTO COACH VALUES ('1', '�ENOL', 'G�NE�', '01/06/1952', '4.8', '1')," +
                "('2', 'FAT�H', 'TER�M', '04/09/1953', '6', '2')," +
                "('3', 'AYKUT', 'KOCAMAN', '05/04/1965', '4', '3')," +
                "('4', 'ABDULLAH', 'AVCI', '31/07/1963', '3.5', '4')," +
                "('5', 'RIZA', '�ALIMBAY', '02/02/1963', '3', '5')," +
                "('6', 'LEONARDO', 'ARAUJO', '05/09/1969', '3', '6')," +
                "('7', '�RFAN', 'BUZ', '15/04/1967', '2', '7')," +
                "('8', 'PAUL', 'LE GUEN', '01/03/1964', '2.5', '8')," +
                "('9', 'MARIUS', 'SUMUD�CA', '04/03/1971', '2.5', '9')," +
                "('10', 'TAMER', 'TUNA', '01/07/1976', '2', '10')," +
                "('11', 'MUSTAFA RE��T', 'AK�AY', '12/12/1958', '2', '11')," +
                "('12', 'SAFET', 'SUS�C', '13/04/1955', '2', '12')," +
                "('13', '�M�T', '�ZAT', '30/10/1976', '1.5', '13')," +
                "('14', 'EROL', 'BULUT', '30/01/1975', '1.5', '14')," +
                "('15', 'OKAN', 'BURUK', '19/10/1973', '2', '15')," +
                "('16', 'KEMAL', '�ZDE�', '10/05/1970', '1.5', '16')," +
                "('17', 'SAMET', 'AYBABA', '05/09/1956', '1.5', '17')," +
                "('18', 'TONY', 'POPOV�C', '04/07/1973', '1.5', '18');";

            string DefaultPresidents = "INSERT INTO PRESIDENT VALUES ('1', 'F�KRET', 'ORMAN', '04/11/1967', '1')," +
                "('2', 'DURSUN', '�ZBEK', '25/03/1949', '2')," +
                "('3', 'AZ�Z', 'YILDIRIM', '02/11/1952', '3')," +
                "('4', 'G�KSEL', 'G�M��DA�', '10/10/1972', '4')," +
                "('5', 'MUHARREM', 'USTA', '6/12/1965', '5')," +
                "('6', 'AL� �AFAK', '�ZT�RK', '17/02/1984', '6')," +
                "('7', 'SADIK', 'D�K', '01/01/1969', '7')," +
                "('8', 'AL�', 'AY', '04/03/1957', '8')," +
                "('9', 'EROL', 'BED�R', '15/10/1958', '9')," +
                "('10', 'MEHMET', 'SERP�L', '01/01/1955', '10')," +
                "('11', 'FAT�H', 'YILMAZ', '01/01/1971', '11')," +
                "('12', 'HASAN', '�AVU�O�LU', '01/01/1975', '12')," +
                "('13', 'MURAT', 'CAVCAV', '01/01/1963', '13')," +
                "('14', 'AD�L', 'GEVREK', '03/03/1976', '14')," +
                "('15', 'H�SEY�N', 'ERY�KSEL', '02/06/1960', '15')," +
                "('16', 'TURGAY', 'C�NER', '01/03/1956', '16')," +
                "('17', 'MECNUN', 'OTYAKMAZ', '10/02/1965', '17')," +
                "('18', 'FER�DUN', 'TANKUT', '01/01/1951', '18');";

            UserManager.SignupUser(new Credential("fikretorman", "fikretorman", "email", "Fikret Orman", 0, 1));
            UserManager.SignupUser(new Credential("dursunozbek", "dursunozbek", "email", "Dursun �zbek", 0, 2));
            UserManager.SignupUser(new Credential("azizyildirim", "azizyildirim", "email", "Aziz Y�ld�r�m", 0, 3));
            UserManager.SignupUser(new Credential("gokselgumusdag", "gokselgumusdag", "email", "G�ksel G�m��da�", 0, 4));
            UserManager.SignupUser(new Credential("muharremusta", "muharremusta", "email", "Muharrem Usta", 0, 5));
            UserManager.SignupUser(new Credential("alisafak", "alisafak", "email", "Ali �afak", 0, 6));
            UserManager.SignupUser(new Credential("sadikdik", "sadikdik", "email", "Sad�k Dik", 0, 7));
            UserManager.SignupUser(new Credential("aliay", "aliay", "email", "Ali Ay", 0, 8));
            UserManager.SignupUser(new Credential("erolbedir", "erolbedir", "email", "Erol Bedir", 0, 9));
            UserManager.SignupUser(new Credential("mehmetserpil", "mehmetserpil", "email", "Mehmet Serpil", 0, 10));
            UserManager.SignupUser(new Credential("fatihyilmaz", "fatihyilmaz", "email", "Fatih Y�lmaz", 0, 11));
            UserManager.SignupUser(new Credential("hasancavusoglu", "hasancavusoglu", "email", "Hasan �avu�o�lu", 0, 12));
            UserManager.SignupUser(new Credential("muratcavcav", "muratcavcav", "email", "Murat Cavcav", 0, 13));
            UserManager.SignupUser(new Credential("adilgevrek", "adilgevrek", "email", "Adil Gevrek", 0, 14));
            UserManager.SignupUser(new Credential("huseyineryuksel", "huseyineryuksel", "email", "H�seyin Ery�ksel", 0, 15));
            UserManager.SignupUser(new Credential("turgayciner", "turgayciner", "email", "Turgay Ciner", 0, 16));
            UserManager.SignupUser(new Credential("mecnunotyakmaz", "mecnunotyakmaz", "email", "Mecnun Otyakmaz", 0, 17));
            UserManager.SignupUser(new Credential("feriduntankut", "feriduntankut", "email", "Feridun Tankut", 0, 18));


            string DefaultPlayers1 = "INSERT INTO PLAYER VALUES ('1', 'ATIBA', 'HUTCHINSON', '08/02/1983', 'CDM', '1.5', '1.5', '1')," +
                "('2', 'FABRICIO', 'RAMIREZ', '31/12/1987', 'GK', '2.75', '1.5', '1')," +
                "('3', 'O�UZHAN', '�ZYAKUP', '23/09/1992', 'CM', '12', '1.5', '1')," +
                "('4', 'FERNANDO', 'MUSLERA', '16/06/1986', 'GK', '11', '4', '2')," +
                "('5', 'BAFET�MB�', 'GOM�S', '06/08/1985', 'ST', '4', '4', '2')," +
                "('6', 'YAS�N', '�ZTEK�N', '19/03/1987', 'LM', '3.5', '4', '2')," +
                "('7', 'MARTIN', 'SKRTEL', '15/12/1984', 'CB', '4.5', '3', '3')," +
                "('8', '�SMA�L', 'K�YBA�I', '10/07/1989', 'LB', '3', '3', '3')," +
                "('9', 'OZAN', 'TUFAN', '23/03/1995', 'CM', '5', '3', '3');";

            DoCommand(DefaultTeams);
            DoCommand(DefaultCoaches);
            DoCommand(DefaultPresidents);
            DoCommand(DefaultPlayers1);
        }
    }
}
