using Library.Models.BookModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.BookService
{
    public class BookService : IBookService
    {
        String connectionString = @"data source=CSG-LPTP-62\MSSQLSERVER01;initial catalog = Library; user id = sa; password=Rakshith98;MultipleActiveResultSets=True";
        public Boolean AddBookData(BookModel book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("BookCreate", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("BookID", book.BookID);
                    sqlCommand.Parameters.AddWithValue("Title", book.Title);
                    sqlCommand.Parameters.AddWithValue("Author", book.Author);
                    sqlCommand.Parameters.AddWithValue("Price", book.Price);
                    sqlCommand.Parameters.AddWithValue("Genre", book.Genre);
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<BookModel> GetAllBooks()
        {
            List<BookModel> bookList = new List<BookModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("GetAllBooks", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        BookModel book = new BookModel();
                        book.BookID = reader.GetInt32(0);
                        book.Title = reader.GetString(1);
                        book.Author = reader.GetString(2);
                        book.Price = reader.GetInt32(3);
                        book.Genre = reader.GetString(4);
                        bookList.Add(book);
                    }
                    return bookList;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public BookModel GetBookDetailsByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("GetBookByID", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("BookID", id);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    BookModel book = new BookModel();
                    while (reader.Read())
                    {
                        book.BookID = reader.GetInt32(0);
                        book.Title = reader.GetString(1);
                        book.Author = reader.GetString(2);
                        book.Price = reader.GetInt32(3);
                        book.Genre = reader.GetString(4);
                    }
                    return book;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
