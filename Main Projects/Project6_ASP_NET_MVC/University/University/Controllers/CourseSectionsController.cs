using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Controllers
{
    [Authorize]

    public class CourseSectionsController : Controller
    {
        private readonly AppDbContext _db;

        public CourseSectionsController(AppDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    IEnumerable<CourseSection> courseSections = _db.CourseSections.ToList();
        //    return View(courseSections);
        //}

        public async Task<IActionResult> Index()
        {
            IEnumerable<CourseSection> courseSections = await _db.CourseSections

                .Include(w => w.WaitlistEntries)
                .Include(e => e.Enrollments)
                .Include(c => c.Course)
                .ToListAsync();

            return View(courseSections);
        }

        //==============================
        // Create
        //==============================
        [HttpGet]
        public IActionResult Create()
        {
            var courses = _db.Courses.ToList();
            SelectList selectListItems = new SelectList(courses, "Id", "Code");
            ViewBag.Courses = selectListItems;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CourseSection courseSection)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(courseSection);
            }
            _db.CourseSections.Add(courseSection);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var crs = _db.CourseSections.FirstOrDefault(c => c.Uuid == uuid);
            if (crs == null)
            {
                return NotFound();
            }
            var courses = _db.Courses.ToList();
            SelectList selectListItems = new SelectList(courses, "Id", "Code");
            ViewBag.Courses = selectListItems;
            return View(crs);
        }
        [HttpPost]
        public IActionResult Edit(CourseSection courseSection)
        {
            if (ModelState.IsValid)
            {
                var oldcrs = _db.CourseSections.FirstOrDefault(c => c.Uuid == courseSection.Uuid);
                if (oldcrs == null)
                {
                    return NotFound();
                }

                oldcrs.CourseId = courseSection.CourseId;
                oldcrs.Semester = courseSection.Semester;
                oldcrs.SectionNumber = courseSection.SectionNumber;
                oldcrs.MaxCapacity = courseSection.MaxCapacity;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(courseSection);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var crs = _db.CourseSections.FirstOrDefault(c => c.Uuid == uuid);
            if (crs == null)
            {
                return NotFound();
            }
            return View(crs);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var crs = _db.CourseSections.FirstOrDefault(c => c.Uuid == uuid);
            if (crs == null)
            {
                return NotFound();
            }
            _db.CourseSections.Remove(crs);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
