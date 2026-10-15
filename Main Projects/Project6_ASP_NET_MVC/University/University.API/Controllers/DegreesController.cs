using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DegreesController : ControllerBase
    {
        private readonly IDegreeProgramService _degreeProgramService;

        public DegreesController(IDegreeProgramService degreeProgramService)
        {
            _degreeProgramService = degreeProgramService;
        }

        [HttpGet("GetAllDegreesData")]
        public IActionResult GetAllDegreesData()
        {
            var degreePrograms = _degreeProgramService.GetAllDegreePrograms();
            return Ok(new { message = "Get all Degrees", degreePrograms });
        }


    }
}
