using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Data.ViewModels.Authentication;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksService _booksService;
        public BooksController(BooksService booksService) 
        {
            _booksService = booksService;
        }
        [Authorize(Roles = UserRoles.Author)]
        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allBooks = _booksService.GetAllBooks();
            return Ok(allBooks);
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("get-book-by-id/{id}")] //comes from HTTP request URL
        public IActionResult GetBookById(int id)
        {
            var book = _booksService.GetBookById(id);
            return Ok(book);
        }
        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody]BookVM book) // requests a parameter from req body
        { 
            _booksService.AddBookWithAuthors(book);
            return Ok();
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody]BookVM book) // requests a parameter from req body
        {
            var updatedBook =_booksService.UpdateBookById(id, book);
            return Ok(updatedBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id) // requests a parameter from req body
        {
            _booksService.DeleteBookById(id);
            return Ok();
        }
    }
}
