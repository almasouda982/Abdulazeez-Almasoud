using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _db;

        public CoursesController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Course> courses = _db.Courses.ToList();
            return View(courses);
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
        public IActionResult Create(Course course)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(course);
            }
            _db.Courses.Add(course);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var crs = _db.Courses.Find(Id);
            if (crs == null)
            {
                return NotFound();
            }
            return View(crs);
        }
        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _db.Courses.Update(course);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields."); 
            return View(course);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var crs = _db.Courses.Find(Id);
            if (crs == null)
            {
                return NotFound();
            }
            return View(crs);
        }
        [HttpPost]
        public IActionResult Delete(Course course)
        {
            _db.Courses.Remove(course);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
