using System;
using System.Collections.Generic;
using SqliteDemo.Models.Entity;

namespace SqliteDemo.Models.Repository
{
    /*
     * This class manages CRUD (create, retrieve, update, delete) operations
     * for books.
     */
    public class BookPersistence
    {
        /*
         * Retrieve from the database the book matching the ISBN field of
         * the parameter.
         * Return null if the book can't be found.
         */
        public static Book getBook(Book keyBook)
        {
            string sqlQuery = "select * from book where isbn=" + keyBook.ISBN;
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Book.
            object[] dataRow = rows[0];
            DateTime dateAdded = DateTime.Parse(dataRow[2].ToString());
            Book book = new Book { Title = (string)dataRow[0], ISBN = (decimal)dataRow[1], DateAdded = dateAdded };
            return book;
        }

        /*
         * Add a Book to the database.
         * Return true iff the add succeeds.
         */
         public static bool AddBook(Book book)
        {
            System.Diagnostics.Debug.WriteLine("DateTime: " + book.DateAdded.ToString("yyyy-MM-dd"));

            string sql = "insert into book (title, isbn, dateadded) values ('"
                + book.Title + "', " 
                + book.ISBN + ", '" 
                + book.DateAdded.ToString("yyyy-MM-dd") + "')";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        public static bool DeleteBook(Book book)
        {
            string sql = "delete from book where isbn=" + book.ISBN;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
         * Update a book that is in the database, replacing all field values except
         * the key field.
         * Return false if the book is not found, based on key field match.
         */
         public static bool UpdateBook(Book book)
        {
          
            string sql = "Update book set title='" +  book.Title + "' where isbn=" + book.ISBN;
            int result = RepositoryManager.Repository.DoCommand(sql);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /*
         * Get all Book data from the database and return an array of Books.
         */
        public static List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            string sqlQuery = "select * from book";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {
                DateTime dateAdded = Convert.ToDateTime(dataRow[2]);
                Book book = new Book { Title=(string)dataRow[0], ISBN=(decimal)dataRow[1],
                    DateAdded=dateAdded};
                books.Add(book);
            }

            return books;
        }
    }
}