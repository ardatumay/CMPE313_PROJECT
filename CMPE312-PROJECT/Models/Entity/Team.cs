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
        public string name { get; set; }
        public string city { get; set; }
        public decimal foundation { get; set; }
        public decimal budget { get; set; }
        public decimal numberOfChampionship { get; set; }
        public ArrayList players = new ArrayList();
        
        public Team()
        {

        }

        public Team(String name1)
        {
            name = name1;
        }
    }
}