using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var courses = _courseService.GetAllCourses().Select(c => new
            {
                c.Id,
                c.Uuid,
                c.Code,
                c.Title,
                c.Credits,
                c.Description,
                Books = c.Books.Select(b => new
                {
                    b.Id,
                    b.Uuid,
                    b.Title,
                    b.Author,
                    b.Price,
                    b.ImageUrl
                })
            });
            return Ok(courses);
        }
    }
}
