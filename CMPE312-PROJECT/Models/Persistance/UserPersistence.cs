using System;
using System.Collections.Generic;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Transaction;

namespace CMPE312_PROJECT.Models.Repository
{
    public class UserPersistence
    {
        private static List<User> users;

        static UserPersistence()
        {
            users = new List<User>();
        }
        /*
         * Get one user from the repository, identified by userId
         */
        public static User GetUser(string userId)
        {
            string sqlQuery = "select * from USER where USER_ID='" + userId + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            User user=null;
            if(rows == null)
            {
                return null;
            }
            else
            {
                foreach (object[] dataRow in rows)
                {
                    user = new User { UserID = (String)dataRow[0], Name = (String)dataRow[1], Email = (String)dataRow[2], Salt = (String)dataRow[3], PasswordHash = (String)dataRow[4], IsAdmin = (decimal)dataRow[5], PresidentID = (decimal)dataRow[6], Status = (String)dataRow[7] };
                }

                if (user == null)
                {
                    return null;
                }

                if (userId == user.UserID)
                {
                    return user;
                }
            }
            return null;
        }

        // Not Implemented
        public static bool UpdateUser(User user1)
        {

            string sql = "Update USER set USER_ID='" + user1.UserID + "', NAME='" + user1.Name + "', EMAIL='" + user1.Email + "', SALT='" + user1.Salt + "', HASHEDPASSWORD='" + user1.PasswordHash + "', IS_ADMIN='" + user1.IsAdmin + "', PRESIDENT_ID='" + user1.PresidentID + "', STATUS='" + user1.Status + "' where USER_ID='" + user1.UserID + "'";
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public static bool DeleteUser(User user1)
        {
            string sql = "delete from USER where USER_ID='" + user1.UserID + "'";
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public static bool InsertUser(User user1)
        {
            string sql = "insert into USER VALUES ('" + user1.UserID + "','" + user1.Name + "','" + user1.Email + "','" + user1.Salt + "','" + user1.PasswordHash + "','" + user1.IsAdmin + "','" + user1.PresidentID + "','" + user1.Status + "')";
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }
        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            string sqlQuery = "select * from USER";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {
                User user = new User { UserID = (String)dataRow[0], Name = (String)dataRow[1], Email = (String)dataRow[2], Salt = (String)dataRow[3], PasswordHash = (String)dataRow[4], IsAdmin = (decimal)dataRow[5], PresidentID = (decimal)dataRow[6], Status = (String)dataRow[7] };
                users.Add(user);
            }

            return users;
        }

        public static decimal GetNumberOfInactive()
        {
            string sqlQuery = "SELECT COUNT(*) FROM USER WHERE STATUS ='I'";
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

        public static decimal GetNumberOfActive()
        {
            string sqlQuery = "SELECT COUNT(*) FROM USER WHERE STATUS ='A'";
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

        public static decimal GetNumberOfUsers()
        {
            string sqlQuery = "SELECT COUNT(*) FROM USER";
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
