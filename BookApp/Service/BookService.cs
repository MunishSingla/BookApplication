using BookApp.Factory;
using BookApp.Interface;
using BookApp.Models;
using System.Collections.Generic;

namespace BookApp.Service
{
    public class BookService : IBookService
    {
        private readonly IBookDbContext _bookDbContext;
        public BookService()
        {
            _bookDbContext = BookDbContextFactory.GetInstance();
        }


        public IEnumerable<Book> GetBooks()
        {
            return _bookDbContext.GetBooks();
        }

        public Book GetbyId(int id)
        {
            return _bookDbContext.GetbyId(id);
        }

        public int AddBook(Book book)
        {
            return _bookDbContext.AddBook(book);
        }

        public int UpdateBook(Book book)
        {
            return _bookDbContext.UpdateBook(book);
        }

        public int Delete(int bookId)
        {
            return _bookDbContext.Delete(bookId);
        }

        public IEnumerable<Book> Search(Book book)
        {
            return _bookDbContext.Search(book);
        }
    }
}

