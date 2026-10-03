using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
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

        public async Task<IActionResult> Index()
        {
            IEnumerable<WaitlistEntry> waitlistEntries = await _db.WaitlistEntries.Include(w => w.Student).Include(w => w.CourseSection).ToListAsync();
            return View(waitlistEntries);
        }

        //==============================
        // Create
        //==============================
        [HttpGet]
        public IActionResult Create()
        {
            var students = _db.Students.ToList();
            SelectList selectListItems1 = new SelectList(students, "Id", "LastName");
            ViewBag.Students = selectListItems1;

            return View();;
        }
        [HttpPost]
        public IActionResult Create(WaitlistEntry waitlistEntry)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(waitlistEntry);
            }
            _db.WaitlistEntries.Add(waitlistEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
            return View(wti);
        }
        [HttpPost]
        public IActionResult Edit(WaitlistEntry waitlistEntry)
        {
            if (ModelState.IsValid)
            {
                var oldwti = _db.WaitlistEntries.FirstOrDefault(o => o.Uuid == waitlistEntry.Uuid);
                if (oldwti == null)
                {
                    return NotFound();
                }

                oldwti.StudentId = waitlistEntry.StudentId;
                oldwti.CourseSectionId = waitlistEntry.CourseSectionId;
                oldwti.Position = waitlistEntry.Position;
                oldwti.OfferedAt = waitlistEntry.OfferedAt;
                oldwti.ExpiresAt = waitlistEntry.ExpiresAt;
                oldwti.Status = waitlistEntry.Status;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(waitlistEntry);
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
            return View(wti);
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
    }
}
