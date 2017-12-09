using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Team
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public DateTime foundation { get; set; }
        public long budget { get; set; }
        public int numberOfChampionship { get; set; }
        public ArrayList players = new ArrayList();
        
        public Team()
        {

        }
    }
}