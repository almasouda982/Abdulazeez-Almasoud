using Microsoft.AspNetCore.Mvc;
using University.Application.Dtos;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseBooksController : ControllerBase
    {
        private readonly ICourseBookService _courseBookService;

        public CourseBooksController(ICourseBookService courseBookService)
        {
            _courseBookService = courseBookService;
        }

        [HttpGet("{courseId}")]
        public IActionResult GetBooksForCourse(int courseId)
        {
            var courseBooks = _courseBookService.GetBooksForCourse(courseId);
            return Ok(courseBooks);
        }

        [HttpPost]
        public IActionResult AssignBookToCourse([FromBody] AssignBookToCourseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _courseBookService.AddBookToCourse(request);
                return Ok(new { message = "Book successfully stacked to course." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{courseId}/{bookId}")]
        public IActionResult RemoveBookFromCourse(int courseId, int bookId)
        {
            try
            {
                _courseBookService.RemoveBookFromCourse(courseId, bookId);
                return Ok(new { message = "Book successfully removed from course." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
