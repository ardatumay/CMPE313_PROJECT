using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPE312_PROJECT.Models.Repository
{

    /*
     * This interface is intended for Persistence classes to use in order
     * to access database functions. 
     * 
     * Use of an interface enables the easy replacement of the actual
     * repository class in the future.
     * 
     * This class was given to us by Mr. Grove and we used it in our project with some changes of course.
     */
    public interface IRepository
    {
        bool IsOpen { get; }

        /* Initialize the database */
        bool Initialize();

        /* Open the database */
        bool Open();

        /* Close the database */
        void Close();

        /* Execute an SQL command */
        int DoCommand(string sqlCommand);

        /* Execute an SQL query */
        List<object[]> DoQuery(string sqlQuery);
    }
}
