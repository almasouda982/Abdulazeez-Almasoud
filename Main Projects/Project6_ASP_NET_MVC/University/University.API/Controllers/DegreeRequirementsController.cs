using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DegreeRequirementsController : ControllerBase
    {
        private readonly IDegreeRequirementService _degreeRequirementService;
        public DegreeRequirementsController(IDegreeRequirementService degreeRequirementService)
        {
            _degreeRequirementService = degreeRequirementService;
        }

        [HttpGet]
        public IActionResult GetAllDegreeRequirements()
        {
            var degreeRequirements = _degreeRequirementService.GetAllDegreeRequirements().Select(dr => new
            {
                dr.Id,
                dr.Uuid,
                DegreeProgram = dr.Name,
                dr.CategoryName,
                dr.RequiredCredits,
                dr.MinLevel,
            });
            return Ok(degreeRequirements);
        }
    }
}
