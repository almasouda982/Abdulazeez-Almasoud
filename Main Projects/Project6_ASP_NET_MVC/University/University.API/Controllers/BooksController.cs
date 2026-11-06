using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks().Select(b => new
            {
                b.Id,
                b.Uuid,
                b.Title,
                b.Author,
                b.Price,
                b.ImageUrl
            });
            return Ok(books);
        }
    }
}
