using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace CMPE312_PROJECT.Models.Entity
{
    /*
    * This class is created for storing Team data in the application after getting it from the database.
    */
    public class Team
    {
        public decimal ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Foundation { get; set; }
        public decimal Budget { get; set; }
        public decimal NumberOfChampionship { get; set; }
        public ArrayList Players = new ArrayList();
        public decimal Point { get; set; }

        public Team()
        {

        }

        public Team(String name1)
        {
            Name = name1;
        }
    }
}