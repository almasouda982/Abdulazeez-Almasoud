using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentsService _enrollmentsService;
        public EnrollmentsController(IEnrollmentsService enrollmentsService)
        {
            _enrollmentsService = enrollmentsService;
        }
        [HttpGet]
        public IActionResult GetAllEnrollments()
        {
            var enrollments = _enrollmentsService.GetAllEnrollments().Select(e => new
            {
                e.Id,
                e.Uuid,
                e.LastName,
                e.SectionNumber,
                e.FinalGrade,
                e.LetterGradePoints,
                e.Status,
                e.DocumentUrl,
            });
            return Ok(enrollments);
        }
    }
}
