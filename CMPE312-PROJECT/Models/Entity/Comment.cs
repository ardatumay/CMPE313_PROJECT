using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    /*
    * This class is created for storing Comment data in the application after getting it from the database.
    */
    public class Comment
    {
        public decimal CommentId { get; set; }
        public string CommentValue { get; set; }
        public decimal PlayerId { get; set; }
        public decimal CoachId { get; set; }
        public decimal PresidentId { get; set; }
        public string TeamName { get; set; }
        public decimal TeamID { get; set; }

        public Comment()
        {

        }
    }
}