using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMPE312_PROJECT.Models.Entity;
using CMPE312_PROJECT.Models.Repository;
using CMPE312_PROJECT.Models.Transaction;
using CMPE312_PROJECT.Models.Persistance;

namespace CMPE312_PROJECT.Models.Transaction
{
    public class CommentManager
    {
        public static bool AddComment(Comment comment)
        {
            bool isAdded;
            isAdded = CommentPersistence.addComment(comment);
            return isAdded;
        }


    }
}