using BookApp.DbContext;
using BookApp.Interface;

namespace BookApp.Factory
{
    public class BookDbContextFactory
    {
        public static IBookDbContext GetInstance()
        {
            return new BookDbContext();
        }
    }
}