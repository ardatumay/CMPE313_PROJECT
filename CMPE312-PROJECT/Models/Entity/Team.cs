using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Team
    {
        int ID { get; set; }
        string name { get; set; }
        string city { get; set; }
        DateTime foundation { get; set; }
        long budget { get; set; }
        int numberOfChampionship { get; set; }
        ArrayList players = new ArrayList();
        
        public Team()
        {

        }
    }
}