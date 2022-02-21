using BookApp.Interface;
using BookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Threading.Tasks;

namespace BookApp.DbContext
{
    public class BookDbContext : IBookDbContext
    {
        private const string crud_books = "Crud_Books";
        public IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>();

            var con = SqlDbConnect.CreateConnection();
            var cmd = SqlDbConnect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = crud_books;
            cmd.Parameters.AddWithValue("@mode", 4);
            cmd.Connection = con;
            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string authors = "";
                    var book = new Book();
                    book.BookId = (reader["BookId"] != DBNull.Value ? Convert.ToInt32(reader["BookId"]) : 0);
                    book.Title = (reader["Title"] != DBNull.Value ? Convert.ToString(reader["Title"]) : string.Empty);
                    book.Isbn = (reader["ISBN"] != DBNull.Value ? Convert.ToString(reader["ISBN"]) : string.Empty);
                    book.PublicationDate = reader["PublicationDate"] != DBNull.Value ? ((DateTime)reader["PublicationDate"]).Date : default(DateTime);
                    authors = (reader["Authors"] != DBNull.Value ? Convert.ToString(reader["Authors"]) : string.Empty);
                    book.Author = authors.Split(',').ToList<string>();

                    books.Add(book);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return books;
        }

        public Book GetbyId(int id)
        {
            var con = SqlDbConnect.CreateConnection();
            var cmd = SqlDbConnect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = crud_books;
            cmd.Parameters.AddWithValue("@mode", 5);
            cmd.Parameters.AddWithValue("@bookId", id);
            cmd.Connection = con;
            var book = new Book();
            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string authors = "";
                    book.BookId = (reader["BookId"] != DBNull.Value ? Convert.ToInt32(reader["BookId"]) : 0);
                    book.Title = (reader["Title"] != DBNull.Value ? Convert.ToString(reader["Title"]) : string.Empty);
                    book.Isbn = (reader["ISBN"] != DBNull.Value ? Convert.ToString(reader["ISBN"]) : string.Empty);
                    book.PublicationDate = reader["PublicationDate"] != DBNull.Value ? ((DateTime)reader["PublicationDate"]).Date : default(DateTime);
                    authors = (reader["Authors"] != DBNull.Value ? Convert.ToString(reader["Authors"]) : string.Empty);
                    book.Author = authors.Split(',').ToList<string>();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return book;
        }

        public int AddBook(Book book)
        {
            var con = SqlDbConnect.CreateConnection();
            var cmd = SqlDbConnect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = crud_books;
            string authors = string.Join(",", book.Author);
            cmd.Parameters.AddWithValue("@mode", 1);
            cmd.Parameters.AddWithValue("@title", book.Title);
            cmd.Parameters.AddWithValue("@author", authors);
            cmd.Parameters.AddWithValue("@isbn", book.Isbn);
            cmd.Parameters.AddWithValue("@publicationDate", book.PublicationDate);
            cmd.Connection = con;
            int result = 0;

            try
            {
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public int UpdateBook(Book book)
        {
            var con = SqlDbConnect.CreateConnection();
            var cmd = SqlDbConnect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = crud_books;
            string authors = string.Join(",", book.Author);
            authors = authors.TrimEnd(',');
            cmd.Parameters.AddWithValue("@mode", 2);
            cmd.Parameters.AddWithValue("@bookId", book.BookId);
            cmd.Parameters.AddWithValue("@title", book.Title);
            cmd.Parameters.AddWithValue("@author", authors);
            cmd.Parameters.AddWithValue("@isbn", book.Isbn);
            cmd.Parameters.AddWithValue("@publicationDate", book.PublicationDate);
            cmd.Connection = con;
            int result = 0;
            try
            {
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public int Delete(int bookId)
        {
            var con = SqlDbConnect.CreateConnection();
            var cmd = SqlDbConnect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = crud_books;
            cmd.Parameters.AddWithValue("@mode", 3);
            cmd.Parameters.AddWithValue("@bookId", bookId);
            cmd.Connection = con;
            int result = 0;
            try
            {
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public IEnumerable<Book> Search(Book obj)
        {
            var books = new List<Book>();
            string srchauthors = "";
            var con = SqlDbConnect.CreateConnection();
            var cmd = SqlDbConnect.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = crud_books;
            cmd.Parameters.AddWithValue("@mode", 6);
            if (!string.IsNullOrWhiteSpace(obj.Title))
                cmd.Parameters.AddWithValue("@title", obj.Title);
            if (obj.Author != null && obj.Author.Count > 0)
            {
                srchauthors = string.Join(",", obj.Author);
                cmd.Parameters.AddWithValue("@author", srchauthors);
            }

            if (!string.IsNullOrWhiteSpace(obj.Isbn))
                cmd.Parameters.AddWithValue("@isbn", obj.Isbn);
            cmd.Connection = con;
            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var book = new Book();
                    string authors = "";
                    book.BookId = (reader["BookId"] != DBNull.Value ? Convert.ToInt32(reader["BookId"]) : 0);
                    book.Title = (reader["Title"] != DBNull.Value ? Convert.ToString(reader["Title"]) : string.Empty);
                    authors = (reader["Authors"] != DBNull.Value ? Convert.ToString(reader["Authors"]) : string.Empty);
                    book.Isbn = (reader["ISBN"] != DBNull.Value ? Convert.ToString(reader["ISBN"]) : string.Empty);
                    book.PublicationDate = reader["PublicationDate"] != DBNull.Value ? ((DateTime)reader["PublicationDate"]).Date : default(DateTime);
                    book.Author = authors.Split(',').ToList<string>();
                    books.Add(book);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return books;
        }
    }
}

