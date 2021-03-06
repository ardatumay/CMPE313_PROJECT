﻿using System;
using CMPE312_PROJECT.Models.Repository;

namespace CMPE312_PROJECT.Models.Repository
{
    /*
     * This class instantiates an instance of IRepository, and provides
     * it to Persistence classes as needed.
     */
    public class RepositoryManager
    {
        public static IRepository Repository { get; set; }
          
        /*
         * Create an instance of a concrete Repository and open it. 
         * The Repository should close itself on shutdown.
         */
        static RepositoryManager() {
            Repository = new SqliteRepository();
            Repository.Open();
        }
    }
}