using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitlistEntrysController : ControllerBase
    {
        private readonly IWaitlistEntryService _waitlistEntryService;

        public WaitlistEntrysController(IWaitlistEntryService waitlistEntryService)
        {
            _waitlistEntryService = waitlistEntryService;
        }
        [HttpGet]
        public IActionResult GetAllWaitlistEntries()
        {
            var waitlistEntries = _waitlistEntryService.GetAllWaitlistEntries().Select(w => new
            {
                w.Id,
                w.Uuid,
                Student = w.LastName,
                CourseSection = w.SectionNumber,
                w.Position,
                w.OfferedAt,
                w.ExpiresAt,
                w.Status,
            });
            return Ok(waitlistEntries);
        }
    }
}
