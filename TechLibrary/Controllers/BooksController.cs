using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TechLibrary.Domain;
using TechLibrary.Models;
using TechLibrary.Services;

namespace TechLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all books");

            var books = await _bookService.GetBooksAsync();

            var bookResponse = _mapper.Map<List<BookResponse>>(books);

            return Ok(bookResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Get book by id {id}");

            var book = await _bookService.GetBookByIdAsync(id);

            var bookResponse = _mapper.Map<BookResponse>(book);

            return Ok(bookResponse);
        }

        [HttpGet("page/{page}/{rows}/{query?}")]
        public async Task<IActionResult> GetPage(int page, int rows, string query = "")
        {
            // Controller methods like this should have standardized
            // try catch blocks with error handling
            // but since that is not in the exercise I will leave it out

            _logger.LogInformation($"Get book page {page} {rows}");

            if (query.Length > 0)
                query = System.Web.HttpUtility.HtmlDecode(query);

            var books = await _bookService.GetBooksPagination(page, rows, query);

            var bookResponse = new
            {
                books = _mapper.Map<List<BookResponse>>(books.books),
                count = books.count
            };

            return Ok(bookResponse);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditBook(Book book)
        {
            if (!ModelState.IsValid || book.BookId == 0)
            {
                return BadRequest(book);
            }
            var updated = await _bookService.EditBook(book);

            return Ok(updated);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(book);
            }
            var added = await _bookService.AddBook(book);

            return Ok(added);
        }
    }
}
