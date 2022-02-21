
using BookApp.Factory;
using BookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookApp.Controllers
{
    public class BookController : ApiController
    {
        private readonly IBookService _bookService;
        public BookController()
        {
            _bookService = BookServiceFactory.GetInstance();
        }

        [HttpGet]
        [Route("api/book")]
        public IEnumerable<Book> GetBooks()
        {
            var books = _bookService.GetBooks();
            return books;
        }

        [HttpGet]
        [Route("api/book/{bookId}")]
        public Book GetbyId(int bookId)
        {
            var book = _bookService.GetbyId(bookId);
            return book;
        }

        [HttpPost]
        [Route("api/book")]
        public HttpResponseMessage AddBook(Book book)
        {
            ArgumentRequired(book, "book object required for POST");
            int result = 0;
            if (ModelState.IsValid)
            {
                result = _bookService.AddBook(book);
            }

            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Book added succesfully");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to add a book");
            }

        }

        [HttpPut]
        [Route("api/book")]
        public HttpResponseMessage UpdateBook(Book obj)
        {
            ArgumentRequired(obj, "book object required for Update");
            int result = 0;
            var book = new Book();
            book = _bookService.GetbyId(obj.BookId);
            if (obj.Author.Count > 0)
            {
                book.Author = obj.Author;
            }
            if (!string.IsNullOrWhiteSpace(obj.Title))
                book.Title = obj.Title;
            if (!string.IsNullOrWhiteSpace(obj.Isbn))
                book.Isbn = obj.Isbn;
            if (obj.PublicationDate != DateTime.MinValue)
                book.PublicationDate = obj.PublicationDate;
            result = _bookService.UpdateBook(book);

            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, book);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to update book");
            }
        }

        [HttpDelete]
        [Route("api/book/{bookId}")]
        public HttpResponseMessage Delete(int bookId)
        {
            int result = 0;
            if (bookId > 0)
            {
                var book = _bookService.GetbyId(bookId);
                if (book != null)
                {
                    result = _bookService.Delete(bookId);
                }
            }
            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete book");
            }
        }

        [HttpPost]
        [Route("api/book/search")]
        public HttpResponseMessage Search(Book book)
        {
            ArgumentRequired(book, "book object required for Update");
            var books = _bookService.Search(book);

            if (books.Count() > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, books);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No matching records found");
            }
        }

        private void ArgumentRequired(dynamic parameter, string message)
        {
            if (parameter == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }
        }
    }
}
