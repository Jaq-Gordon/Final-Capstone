using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class BookDAO : IBookDAO
    {
        private readonly string sqlGetBookID = @"SELECT book_id FROM book where isbn = @isbn";
        private readonly string sqlAddNewBook = @"INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES (@title, @firstName, @lastName, @isbn); SELECT @@IDENTITY";
        private readonly string sqlAddToPersonalLibrary = @"INSERT  personal_library(user_id, book_id, isCompleted) 
                                                            SELECT  @user_id, @book_id, 0
                                                            WHERE   NOT EXISTS 
                                                                    (   SELECT  1
                                                                        FROM    personal_library 
                                                                        WHERE   user_id = @user_id 
                                                                        AND     book_id = @book_id
                                                                    );";
        private readonly string sqlGetFamilyBooks = @"SELECT DISTINCT
	                                                    b.book_id AS book_id,
	                                                    b.title AS title,
	                                                    b.author_firstName AS author_first,
	                                                    b.author_lastName AS author_last,
	                                                    b.isbn AS isbn
                                                    FROM
	                                                    users u
	                                                    INNER JOIN family_library fl ON u.family_id = fl.family_id
	                                                    INNER JOIN book b ON fl.book_id = b.book_id
                                                    WHERE
	                                                    u.user_id = @user_id;";
        private readonly string sqlGetPersonalLibrary = @"SELECT DISTINCT
	                                                        pl.id AS pl_id,
                                                            b.book_id AS book_id,
	                                                        b.title AS title,
	                                                        b.author_firstName AS a_first,
	                                                        b.author_lastName AS a_last,
	                                                        b.isbn AS isbn,
	                                                        pl.isCompleted AS is_completed
                                                        FROM
	                                                        personal_library pl
	                                                        INNER JOIN users u ON pl.user_id = u.user_id
	                                                        INNER JOIN book b ON pl.book_id = b.book_id
                                                        WHERE
	                                                        u.user_id = @userId";

        private readonly string connectionString;

        public BookDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int CheckIfBookExistsOnDB(Book inputBook)
        {
            int bookID = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetBookID, conn);
                cmd.Parameters.AddWithValue("@isbn", inputBook.ISBN);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    bookID = Convert.ToInt32(reader["book_id"]);
                }
            }
             return bookID;
        }

        public int AddNewBook(Book newBook)
        {
            int bookID = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlAddNewBook, conn);
                cmd.Parameters.AddWithValue("@title", newBook.Title);
                cmd.Parameters.AddWithValue("@firstName", newBook.AuthorFirstName);
                cmd.Parameters.AddWithValue("@lastName", newBook.AuthorLastName);
                cmd.Parameters.AddWithValue("@isbn", newBook.ISBN);

                bookID = Convert.ToInt32(cmd.ExecuteScalar());

            }

                return bookID;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT * FROM book", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookId = Convert.ToInt32(reader["book_id"]);
                    book.Title = Convert.ToString(reader["title"]);
                    book.AuthorFirstName = Convert.ToString(reader["author_firstName"]);
                    book.AuthorLastName = Convert.ToString(reader["author_lastName"]);
                    book.ISBN = Convert.ToInt64(reader["isbn"]);

                    books.Add(book);
                }

                return books;

            }

        }

        public Book GetSpecificBook(long isbn)
        {
            Book book = new Book();
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT * FROM book WHERE isbn = @isbn", conn);
                cmd.Parameters.AddWithValue("@isbn", isbn);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    book.BookId = Convert.ToInt32(reader["book_id"]);
                    book.Title = Convert.ToString(reader["title"]);
                    book.AuthorFirstName = Convert.ToString(reader["author_firstName"]);
                    book.AuthorLastName = Convert.ToString(reader["author_lastName"]);
                    book.ISBN = Convert.ToInt64(reader["isbn"]);

                    return book;
                }
                else
                {
                    return null;
                }

            }
        }

        public bool AddToFamilyLibrary(int bookID, int userId)
        {
            int rowsChanged = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"INSERT INTO family_library(family_id, book_id) VALUES ((SELECT family_id FROM users WHERE user_id = @user_id), @book_id);", conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@book_id", bookID);

                rowsChanged = cmd.ExecuteNonQuery();

                if(rowsChanged == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public List<Book> GetAllFamilyBooks(int id)
        {
            List<Book> books = new List<Book>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetFamilyBooks, conn);
                cmd.Parameters.AddWithValue("@user_id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookId = Convert.ToInt32(reader["book_id"]);
                    book.Title = Convert.ToString(reader["title"]);
                    book.AuthorFirstName = Convert.ToString(reader["author_first"]);
                    book.AuthorLastName = Convert.ToString(reader["author_last"]);
                    book.ISBN = Convert.ToInt64(reader["isbn"]);

                    books.Add(book);
                }

                return books;

            }
        }
        
        public List<PersonalBook> GetPersonalBooks(int userId)
        {
            List<PersonalBook> books = new List<PersonalBook>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetPersonalLibrary, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PersonalBook book = new PersonalBook();
                    book.PersonalLibraryId = Convert.ToInt32(reader["pl_id"]);
                    book.BookId = Convert.ToInt32(reader["book_id"]);
                    book.Title = Convert.ToString(reader["title"]);
                    book.AuthorFirstName = Convert.ToString(reader["a_first"]);
                    book.AuthorLastName = Convert.ToString(reader["a_last"]);
                    book.ISBN = Convert.ToInt64(reader["isbn"]);
                    book.IsCompleted = Convert.ToInt32(reader["is_completed"]);

                    books.Add(book);
                   

                }
            }

            return books;
        }
        
        public bool AddPersonalBook(int userId, int bookId)
        {
            int rowsChanged = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlAddToPersonalLibrary, conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@book_id", bookId);

                rowsChanged = cmd.ExecuteNonQuery();

                if (rowsChanged == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
