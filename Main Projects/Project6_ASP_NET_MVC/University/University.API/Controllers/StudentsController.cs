using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentService.GetAllStudents().Select(s => new
            {
                s.Id,
                s.Name,
                s.Uuid,
                s.FirstName,
                s.LastName,
                s.Email,
                s.Phone,
                s.Standing,
                s.GPA,
                s.EnrollmentStatus,
                s.ImageUrl
            });
            return Ok(students);
        }
    }
}
