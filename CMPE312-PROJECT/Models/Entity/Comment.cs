using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPE312_PROJECT.Models.Entity
{
    public class Comment
    {
        public decimal commentId { get; set; }
        public string comment { get; set; }
        public decimal playerId { get; set; }
        public decimal coachId { get; set; }
        public decimal presidentId { get; set; }
        public string teamName { get; set; }
        public decimal teamID { get; set; }

        public Comment()
        {

        }
    }
}