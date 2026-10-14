using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Dtos;
using University.Models;
using University.Repositories;
using University.Services.Base;

namespace University.Controllers
{
    [Authorize]

    public class WaitlistEntrysController : Controller
    {
        //private readonly IWaitlistEntryRepository _waitlistEntryRepository;
        private readonly IWaitlistEntryService _waitlistEntryService;

        public WaitlistEntrysController(IWaitlistEntryService waitlistEntryService)
        {
            _waitlistEntryService = waitlistEntryService;
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
            var waitlistEntriesDto = _waitlistEntryService.GetAllWaitlistEntries();
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
                _waitlistEntryService.AddWaitlistEntry(waitlistEntryCreateDto);
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
            var wti = _waitlistEntryService.GetWaitlistEntryByUuid(uuid);
            if (wti == null)
            {
                return NotFound();
            }

            LoadWatilistEntry();
            return View(wti);
        }

        [HttpPost]
        public IActionResult Edit(WaitlistEntryUpdateDto waitlistEntryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _waitlistEntryService.UpdateWaitlistEntry(waitlistEntryUpdateDto);
                    return RedirectToAction("Index");
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
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
            var wti = _waitlistEntryService.GetWaitlistEntryByUuid(uuid);
            if (wti == null)
            {
                return NotFound();
            }

            return View(wti);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var wti = _waitlistEntryService.GetWaitlistEntryByUuid(uuid);
            if (wti == null)
            {
                return NotFound();
            }



            _waitlistEntryService.DeleteWaitlistEntry(uuid);
            return RedirectToAction("Index");
        }

        public void LoadWatilistEntry()
        {
            var students = _waitlistEntryService.GetStudents();
            SelectList selectListItems1 = new SelectList(students, "Id", "LastName");
            ViewBag.Students = selectListItems1;
            var courseSections = _waitlistEntryService.GetCourseSections();
            SelectList selectListItems2 = new SelectList(courseSections, "Id", "SectionNumber");
            ViewBag.CourseSections = selectListItems2;
        }
    }
}
