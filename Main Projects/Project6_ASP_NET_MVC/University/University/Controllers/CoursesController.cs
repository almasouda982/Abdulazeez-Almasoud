using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _db;

        public CoursesController(AppDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    IEnumerable<Course> courses = _db.Courses.ToList();
        //    return View(courses);
        //}


        public async Task<IActionResult> Index()
        {
            IEnumerable<Course> courses = await _db.Courses

                .Include(cp => cp.CoursePrerequisites)
                .Include(d => d.CourseSections).ToListAsync();

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
        public IActionResult Edit(string uuid)
        {
            var crs = _db.Courses.FirstOrDefault(c => c.Uuid == uuid);
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
                var oldcrs = _db.Courses.FirstOrDefault(o => o.Uuid == course.Uuid);
                if (oldcrs == null)
                {
                    return NotFound();
                }

                oldcrs.Code = course.Code;
                oldcrs.Title = course.Title;
                oldcrs.Credits = course.Credits;
                oldcrs.Description= course.Description;

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
        public IActionResult Delete(string uuid)
        {
            var crs = _db.Courses.FirstOrDefault(c => c.Uuid ==uuid);
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
            var crs = _db.Courses.FirstOrDefault(c => c.Uuid == uuid);
            if (crs == null)
            {
                return NotFound();
            }

            _db.Courses.Remove(crs);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
