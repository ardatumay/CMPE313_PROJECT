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
                    user = new User { userID = (String)dataRow[0], name = (String)dataRow[1], email = (String)dataRow[2], salt = (String)dataRow[3], passwordHash = (String)dataRow[4], isAdmin = (int)dataRow[5], status = (String)dataRow[6] };
                }
                if (userId == user.userID)
                {
                    return user;
                }
            }
            return null;
        }

        // Not Implemented
        public static bool UpdateUser(User user1)
        {

            string sql = "Update USER set USER_ID='" + user1.userID + "', NAME='" + user1.name + "', EMAIL='" + user1.email + "', IS_ADMIN='" + user1.isAdmin + "', STATUS='" + user1.status + "' where USER_ID=" + user1.userID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }
        public static bool DeleteUser(User user1)
        {
            string sql = "delete from USER where USER_ID=" + user1.userID;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }
        public static bool InsertUser(User user1)
        {
            string sql = "insert into USER (USER_ID, NAME, EMAIL, SALT, HASHEDPASSWORD, IS_ADMIN, STATUS) VALUES ('" + user1.userID + "','" + user1.name + "','" + user1.email + "','" + user1.salt + "','" + user1.passwordHash + "','" + user1.isAdmin + "','" + user1.status + "')";
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
                User user = new User { userID = (String)dataRow[0], name = (String)dataRow[1], email = (String)dataRow[2], salt = (String)dataRow[3], passwordHash = (String)dataRow[4], isAdmin = (int)dataRow[5], status = (String)dataRow[6] };
                users.Add(user);
            }

            return users;
        }
    }
}
