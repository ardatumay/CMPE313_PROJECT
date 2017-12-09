using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class President
    {
        int ID { get; set; }
        string name { get; set; }
        string surname { get; set; }
        DateTime bithDate { get; set; }
        Team team { get; set; }

        public President()
        {

        }
    }
}