using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Dtos;
using University.Models;

namespace University.Controllers
{
    [Authorize]

    public class WaitlistEntrysController : Controller
    {
        private readonly AppDbContext _db;

        public WaitlistEntrysController(AppDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    IEnumerable<WaitlistEntry> waitlistEntries = _db.WaitlistEntries.ToList();
        //    return View(waitlistEntries);
        //}

        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<WaitlistEntry> waitlistEntries = await _db.WaitlistEntries
        //        .Include(w => w.Student)
        //        .Include(w => w.CourseSection).ToListAsync();
        //    return View(waitlistEntries);
        //}

        // select via a Dto
        
        public IActionResult Index()
        {
            IEnumerable<WaitlistEntryDto> waitlistEntriesDto =  _db.WaitlistEntries.Select(w=> new WaitlistEntryDto
            {
                // Mapping properties from WaitlistEntry to WaitlistEntryDto
                Id = w.Id,
                Uuid = w.Uuid,
                LastName = w.Student != null ? w.Student.LastName : null,
                SectionNumber = w.CourseSection != null ? w.CourseSection.SectionNumber : (int?)null,
                Position = w.Position,
                OfferedAt = w.OfferedAt,
                ExpiresAt = w.ExpiresAt,
                Status = w.Status

            }
            ).ToList();
            return View(waitlistEntriesDto);
        }

        //==============================
        // Create
        //==============================
        [HttpGet]
        public IActionResult Create()
        {
            LoadWatilistEntry();
            return View();
        }
        [HttpPost]
        public IActionResult Create(WaitlistEntryCreateDto waitlistEntryCreateDto)
        {
            if (ModelState.IsValid)
            {
                //Mapping the DTO to the WaitlistEntry model
                var waitlistEntry = new WaitlistEntry
                {
                    StudentId = waitlistEntryCreateDto.StudentId,
                    CourseSectionId = waitlistEntryCreateDto.CourseSectionId,
                    Position = waitlistEntryCreateDto.Position,
                    OfferedAt = waitlistEntryCreateDto.OfferedAt,
                    ExpiresAt = waitlistEntryCreateDto.ExpiresAt,
                    Status = waitlistEntryCreateDto.Status
                };
                _db.WaitlistEntries.Add(waitlistEntry);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            LoadWatilistEntry();
            return View(waitlistEntryCreateDto);


        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var wti = _db.WaitlistEntries.FirstOrDefault(w => w.Uuid == uuid);
            if (wti == null)
            {
                return NotFound();
            }
                var dto = new WaitlistEntryUpdateDto
            {
                Uuid = wti.Uuid,
                StudentId = wti.StudentId,
                CourseSectionId = wti.CourseSectionId,
                Position = wti.Position,
                OfferedAt = wti.OfferedAt,
                ExpiresAt = wti.ExpiresAt,
                Status = wti.Status
            };

            LoadWatilistEntry();
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit(WaitlistEntryUpdateDto waitlistEntryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var oldwti = _db.WaitlistEntries.FirstOrDefault(o => o.Uuid == waitlistEntryUpdateDto.Uuid);
                if (oldwti == null)
                {
                    return NotFound();
                }

                oldwti.StudentId = waitlistEntryUpdateDto.StudentId;
                oldwti.CourseSectionId = waitlistEntryUpdateDto.CourseSectionId;
                oldwti.Position = waitlistEntryUpdateDto.Position;
                oldwti.OfferedAt = waitlistEntryUpdateDto.OfferedAt;
                oldwti.ExpiresAt = waitlistEntryUpdateDto.ExpiresAt;
                oldwti.Status = waitlistEntryUpdateDto.Status;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            LoadWatilistEntry();
            return View(waitlistEntryUpdateDto);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var wti = _db.WaitlistEntries.FirstOrDefault(w => w.Uuid == uuid);
            if (wti == null)
            {
                return NotFound();
            }
            var dto = new WaitlistEntryUpdateDto
            {
                Uuid = wti.Uuid,
                StudentId = wti.StudentId,
                CourseSectionId = wti.CourseSectionId,
                Position = wti.Position,
                OfferedAt = wti.OfferedAt,
                ExpiresAt = wti.ExpiresAt,
                Status = wti.Status
            };

            return View(dto);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var wti = _db.WaitlistEntries.FirstOrDefault(w => w.Uuid == uuid);
            if (wti == null)
            {
                return NotFound();
            }



            _db.WaitlistEntries.Remove(wti);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void LoadWatilistEntry()
        {
            var students = _db.Students.ToList();
            SelectList selectListItems1 = new SelectList(students, "Id", "LastName");
            ViewBag.Students = selectListItems1;
            var courseSections = _db.CourseSections.ToList();
            SelectList selectListItems2 = new SelectList(courseSections, "Id", "SectionNumber");
            ViewBag.CourseSections = selectListItems2;
        }
    }
}
