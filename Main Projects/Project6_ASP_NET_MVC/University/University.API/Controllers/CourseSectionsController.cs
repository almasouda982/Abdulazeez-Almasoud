using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseSectionsController : ControllerBase
    {

        private readonly ICourseSectionService _courseSectionService;
        public CourseSectionsController(ICourseSectionService courseSectionService)
        {
            _courseSectionService = courseSectionService;
        }
        [HttpGet]
        public IActionResult GetAllCourseSections()
        {
            var courseSections = _courseSectionService.GetAllCourseSections().Select(cs => new
            {
                cs.Id,
                cs.Uuid,
                Course = cs.Code,
                cs.Semester,
                cs.SectionNumber,
                cs.MaxCapacity,
                cs.DaysOfWeek,
                cs.TimeSlot,

            });
            return Ok(courseSections);
        }

    }
}
