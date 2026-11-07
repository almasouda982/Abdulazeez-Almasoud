using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Application.Dtos;
using University.Application.Services.Base;

namespace University.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var bookDtos = _bookService.GetAllBooks();
            return View(bookDtos);
        }

        //==============================
        // Create
        //==============================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookCreateDto bookCreateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.AddBook(bookCreateDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(bookCreateDto);
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var book = _bookService.GetBookByUuid(uuid);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookUpdateDto bookUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.UpdateBook(bookUpdateDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(bookUpdateDto);
        }

        //==============================
        // Delete
        //==============================
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var book = _bookService.GetBookByUuid(uuid);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(string uuid)
        {
            var book = _bookService.GetBookByUuid(uuid);
            if (book == null)
            {
                return NotFound();
            }

            _bookService.DeleteBook(uuid);
            return RedirectToAction(nameof(Index));
        }
    }
}