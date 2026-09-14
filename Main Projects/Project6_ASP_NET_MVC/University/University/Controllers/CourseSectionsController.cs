using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
    public class CourseSectionsController : Controller
    {
        private readonly AppDbContext _db;

        public CourseSectionsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<CourseSection> courseSections = _db.CourseSections.ToList();
            return View(courseSections);
        }

        //==============================
        // Create
        //==============================
        [HttpGet]
        public IActionResult Create()
        {
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
        public IActionResult Edit(int Id)
        {
            var crs = _db.CourseSections.Find(Id);
            if (crs == null)
            {
                return NotFound();
            }
            return View(crs);
        }
        [HttpPost]
        public IActionResult Edit(CourseSection courseSection)
        {
            if (ModelState.IsValid)
            {
                _db.CourseSections.Update(courseSection);
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
        public IActionResult Delete(int Id)
        {
            var crs = _db.CourseSections.Find(Id);
            if (crs == null)
            {
                return NotFound();
            }
            return View(crs);
        }
        [HttpPost]
        public IActionResult Delete(CourseSection courseSection)
        {
            _db.CourseSections.Remove(courseSection);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
