using BookApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApp.Factory
{
    public class BookServiceFactory
    {
        public static IBookService GetInstance()
        {
            return new BookService();
        }
    }
}