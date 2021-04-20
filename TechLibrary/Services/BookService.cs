using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechLibrary.Data;
using TechLibrary.Domain;
using TechLibrary.Models;

namespace TechLibrary.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int bookid);
        Task<BooksPage> GetBooksPagination(int page, int rows = 10);
    }

    public class BookService : IBookService
    {
        private readonly DataContext _dataContext;

        public BookService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            var queryable = _dataContext.Books.AsQueryable();

            return await queryable.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookid)
        {
            return await _dataContext.Books.SingleOrDefaultAsync(x => x.BookId == bookid);
        }

        /// <summary>
        /// Get Books using pagination based on a current page and the maximum number of rows to return.
        /// </summary>
        /// <param name="page">int - 0 based</param>
        /// <param name="rows">int - default to 10</param>
        /// <returns></returns>
        public async Task<BooksPage> GetBooksPagination(int page, int rows = 10)
        {
            var result = new BooksPage();

            result.count = _dataContext
                .Books
                .Count();

            result.books = await _dataContext
                .Books
                .AsQueryable()
                .Skip(page * rows)
                .Take(rows)
                .ToListAsync();

            return result;
        }
    }
}
