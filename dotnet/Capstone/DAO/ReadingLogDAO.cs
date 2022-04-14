using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class ReadingLogDAO : IReadingLogDAO
    {
        private readonly string connectionString;
        private readonly string sqlGetTotaledReadingLog = @"SELECT
    b.book_id AS bookID,
	b.title as title,
	b.author_firstName AS author_first,
	b.author_lastName AS author_last,
    b.isbn AS isbn,
	SUM(rl.total_time) AS totalTime,
	SUM(CAST (pl.isCompleted AS INT)) AS isCompleted
FROM
	reading_log rl
	INNER JOIN personal_library pl ON rl.personal_library_id = pl.ID
	INNER JOIN book b ON pl.book_id = b.book_id
WHERE
	pl.user_id = @user_id
GROUP BY
    b.book_id,
	b.title,
	b.author_firstName,
	b.author_lastName,
    b.isbn
";
        private readonly string sqlGetLineItemReadingLogs = @"SELECT
    rl.reading_log_id AS log_id,
    pl.user_id AS user_id,
    b.book_id AS book_id,
	b.title AS title,
	b.author_firstName AS author_first,
	b.author_lastName AS author_last,
	b.isbn AS isbn,
	rl.total_time AS total_time,
	rf.format_type AS format_type,
	rl.notes AS note,
	pl.isCompleted AS isCompleted
FROM
	reading_log rl
	INNER JOIN personal_library pl ON rl.personal_library_id = pl.ID
	INNER JOIN book b ON pl.book_id = b.book_id
	INNER JOIN reading_format rf ON rl.format_id = rf.format_id
WHERE
	pl.user_id = @user_id;";

        private readonly string sqlCheckIfBookByISBN = @"SELECT book_Id FROM book WHERE isbn = @isbn";
        private readonly string sqlAddReadingLog = @"INSERT INTO reading_log(personal_library_id, format_id, total_time, notes) VALUES
        (@PL_ID, @formatID, @TotalTime, @Notes ); SELECT @@IDENTITY";
        public ReadingLogDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<ReadingLog> GetUserBooks(int id)
        {
            List<ReadingLog> readingLogs = new List<ReadingLog>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetLineItemReadingLogs, conn);
                cmd.Parameters.AddWithValue("@user_id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    ReadingLog readinglog = new ReadingLog();
                    readinglog.LogID = Convert.ToInt32(reader["log_id"]);
                    readinglog.ReaderId = Convert.ToInt32(reader["user_id"]);
                    readinglog.LoggedBook.BookId = Convert.ToInt32(reader["book_id"]);
                    readinglog.LoggedBook.Title = Convert.ToString(reader["title"]);
                    readinglog.LoggedBook.AuthorFirstName = Convert.ToString(reader["author_first"]);
                    readinglog.LoggedBook.AuthorLastName = Convert.ToString(reader["author_last"]);
                    readinglog.LoggedBook.ISBN = Convert.ToInt64(reader["isbn"]);
                    readinglog.TimeRead = Convert.ToInt32(reader["total_time"]);
                    readinglog.FormatType = Convert.ToString(reader["format_type"]);
                    readinglog.Note = Convert.ToString(reader["note"]);
                    readingLogs.Add(readinglog);
                }
            }

            return readingLogs;
        }

        public int AddNewReadingLog(NewLog newLog)
        {
            int readingLogID = 0;
            int formatID = GetFormatID(newLog.FormatType);
            if (newLog.Note == null)
            {
                newLog.Note = "";
            }
            if(newLog.IsCompleted == 1)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE personal_library SET isCompleted = 1 WHERE ID = @pl_id", conn);
                    cmd.Parameters.AddWithValue("@pl_id", newLog.PersonalLibraryID);
                    cmd.ExecuteNonQuery();
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlAddReadingLog, conn);
                cmd.Parameters.AddWithValue("@PL_ID", newLog.PersonalLibraryID);
                cmd.Parameters.AddWithValue("@formatID", formatID);
                cmd.Parameters.AddWithValue("@TotalTime", newLog.TotalTime);
                cmd.Parameters.AddWithValue("@Notes", TruncateNote(newLog.Note, 255));

                readingLogID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return readingLogID;

        }

        public List<BookHistoryObj> GetUserBookHistory(int userID)
        {
            List<BookHistoryObj> bookHistory = new List<BookHistoryObj>();

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetTotaledReadingLog, conn);
                cmd.Parameters.AddWithValue("@user_id", userID);

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    BookHistoryObj bookLog = new BookHistoryObj();
                    bookLog.BookId = Convert.ToInt32(reader["bookID"]);
                    bookLog.Title = Convert.ToString(reader["title"]);
                    bookLog.AuthorFirstName = Convert.ToString(reader["author_first"]);
                    bookLog.AuthorLastName = Convert.ToString(reader["author_last"]);
                    bookLog.ISBN = Convert.ToInt64(reader["isbn"]);
                    bookLog.TotalTime = Convert.ToInt32(reader["totalTime"]);
                    bookLog.IsCompleted = Convert.ToInt32(reader["isCompleted"]);

                    bookHistory.Add(bookLog);
                }

            }
            return bookHistory;

        }

        private int GetFormatID(string format)
        {
           
            switch (format.ToLower())
            {
                case "paperback":
                    return 1;
                    break;
                case "ebook":
                    return 2;
                    break;
                case "audiobook":
                    return 3;
                    break;
                case "read-aloud (reader)":
                    return 4;
                    break;
                case "read-aloud (listener)":
                    return 5;
                    break;
                default:
                    return 6;
                    break;
            }
        }
        private string TruncateNote(string note, int maxLength)
        {
            if (string.IsNullOrEmpty(note)) return note;
            return note.Length <= maxLength ? note : note.Substring(0, maxLength);
        }

    }
}
