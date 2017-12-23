using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Team
    {
        public decimal ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Foundation { get; set; }
        public decimal Budget { get; set; }
        public decimal NumberOfChampionship { get; set; }
        public ArrayList Players = new ArrayList();
        
        public Team()
        {

        }

        public Team(String name1)
        {
            name = name1;
        }
    }
}