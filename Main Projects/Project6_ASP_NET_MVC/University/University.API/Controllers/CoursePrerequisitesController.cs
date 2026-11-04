using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursePrerequisitesController : ControllerBase
    {
        private readonly ICoursePrerequisiteService _coursePrerequisiteService;
        public CoursePrerequisitesController(ICoursePrerequisiteService coursePrerequisiteService)
        {
            _coursePrerequisiteService = coursePrerequisiteService;
        }
        [HttpGet]
        public IActionResult GetAllCoursePrerequisites()
        {
            var coursePrerequisites = _coursePrerequisiteService.GetAllCoursePrerequisites().Select(cp => new
            {
                cp.Id,
                cp.Uuid,
                cp.MinGradeRequired,
                Course = cp.Code,
                cp.PrerequisiteCourseId
            });
            return Ok(coursePrerequisites);
        }
    }
}
