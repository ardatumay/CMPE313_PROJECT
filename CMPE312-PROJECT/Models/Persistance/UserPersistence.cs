using System;
using System.Collections.Generic;
using Lab7.Models.Entity;
using Lab7.Models.Transaction;

namespace Lab7.Models.Repository
{
    public class UserPersistence
    {
        private static List<User> users;

        static UserPersistence()
        {
            users = new List<User>();

            string salt = EncryptionManager.PasswordSalt;
            users.Add(new User
            {
                UserId = "user1",
                Name = "Alpha Romeo",
                Salt = salt,
                PasswordHash = EncryptionManager.EncodePassword("abc123", salt),
                IsAdmin = false
            });

            salt = EncryptionManager.PasswordSalt;
            users.Add(new User
            {
                UserId = "admin1",
                Name = "Charlie Eagle",
                Salt = salt,
                PasswordHash = EncryptionManager.EncodePassword("abcd1234", salt),
                IsAdmin = true
            });
        }
        /*
         * Get one user from the repository, identified by userId
         */
        public static User GetUser(string userId)
        {
            foreach (User user in users)
            {
                if (userId == user.UserId)
                {
                    return user;
                }
            }
            return null;
        }

        // Not Implemented
        public static bool UpdateUser(User user)
        {
            return false;
        }
        public static bool DeleteUser(User user)
        {
            return false;
        }
        public static List<User> GetAllUsers()
        {
            return null;
        }
    }
}
