﻿using System;
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

            string salt = EncryptionManager.PasswordSalt;
            users.Add(new User ("user1", "Alpha Romeo", salt,(EncryptionManager.EncodePassword("abc123", salt)), false));

            salt = EncryptionManager.PasswordSalt;
            users.Add(new User("admin1", "Charlie Eagle", salt, (EncryptionManager.EncodePassword("abcd1234", salt)), true));
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
