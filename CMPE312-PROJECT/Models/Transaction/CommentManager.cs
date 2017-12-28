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
    /*
    * This class is created for providing connection between model and controller about Comment in some cases.
    */
    public class CommentManager
    {
        /*
        * This method takes a Comment object as parameter and adds this team comment object to the database by using addCommentTeam() method of CommentPersistence class. 
        * If this operation succeeds, the method returns true.
        */
        public static bool AddCommentTeam(Comment comment)
        {
            bool isAdded;
            isAdded = CommentPersistence.addCommentTeam(comment);
            return isAdded;
        }

        /*
        * This method takes a Comment object as parameter and adds this player comment object to the database by using addCommentPlayer() method of CommentPersistence class. 
        * If this operation succeeds, the method returns true.
        */
        public static bool AddCommentPlayer(Comment comment)
        {
            bool isAdded;
            isAdded = CommentPersistence.addCommentPlayer(comment);
            return isAdded;
        }
    }
}