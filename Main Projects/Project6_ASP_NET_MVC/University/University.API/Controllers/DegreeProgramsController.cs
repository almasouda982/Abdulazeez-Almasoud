using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DegreeProgramsController : ControllerBase
    {
        private readonly IDegreeProgramService _degreeProgramService;

        public DegreeProgramsController(IDegreeProgramService degreeProgramService)
        {
            _degreeProgramService = degreeProgramService;
        }

        [HttpGet]
        public IActionResult GetAllDegreePrograms()
        {
            var degreePrograms = _degreeProgramService.GetAllDegreePrograms().Select(dp => new
            {
                dp.Id,
                dp.Uuid,
                dp.Name,
                dp.CatalogYear,
                dp.TotalCreditsRequired
            });
            return Ok(degreePrograms);
        }
    }
}
