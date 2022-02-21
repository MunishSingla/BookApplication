using BookApp.Models;
using System.Collections.Generic;

namespace BookApp.Interface
{
    public interface IBookDbContext
    {

        IEnumerable<Book> GetBooks();
        Book GetbyId(int id);

        int AddBook(Book book);

        int UpdateBook(Book book);

        int Delete(int bookId);

        IEnumerable<Book> Search(Book book);
    }
}

