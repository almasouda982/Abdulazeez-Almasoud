using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Application.Dtos;
using University.Application.Services.Base;

namespace University.Controllers
{
    [Authorize]
    public class CourseBooksController : Controller
    {
        private readonly ICourseBookService _courseBookService;
        private readonly ICourseService _courseService;
        private readonly IBookService _bookService;

        public CourseBooksController(ICourseBookService courseBookService, ICourseService courseService, IBookService bookService)
        {
            _courseBookService = courseBookService;
            _courseService = courseService;
            _bookService = bookService;
        }

        // View stacked books for a specific course
        [HttpGet]
        public IActionResult Index(int courseId)
        {
            var courseBooks = _courseBookService.GetBooksForCourse(courseId);
            ViewBag.CourseId = courseId;

            var allBooks = _bookService.GetAllBooks();
            ViewBag.Books = new SelectList(allBooks, "Id", "Title");
            return View(courseBooks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignBook(AssignBookToCourseRequest request)
        {
            if (ModelState.IsValid)
            {
                _courseBookService.AddBookToCourse(request);
            }
            return RedirectToAction(nameof(Index), new { courseId = request.CourseId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveBook(int courseId, int bookId)
        {
            _courseBookService.RemoveBookFromCourse(courseId, bookId);
            return RedirectToAction(nameof(Index), new { courseId });
        }
    }
}
