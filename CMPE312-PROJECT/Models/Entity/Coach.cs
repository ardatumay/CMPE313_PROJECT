﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Coach
    {
        public decimal ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public decimal Salary { get; set; }
        public decimal TeamID { get; set; }
        public string TeamName { get; set; }

        public Coach()
        {

        }
        public Coach(string name_)
        {
            Name = name_;
        }
    }
}