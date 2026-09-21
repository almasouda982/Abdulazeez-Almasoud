using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
    [Authorize]

    public class CoursePrerequisitsController : Controller
    {
        private readonly AppDbContext _db;

        public CoursePrerequisitsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<CoursePrerequisite> coursePrerequisites = _db.CoursePrerequesites.ToList();
            return View(coursePrerequisites);
        }
        //============
        //Create
        //============
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CoursePrerequisite coursePrerequisite)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(coursePrerequisite);
            }
            _db.CoursePrerequesites.Add(coursePrerequisite);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //============
        //Edit
        //============
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var crsp = _db.CoursePrerequesites.FirstOrDefault(c => c.Uuid == uuid);
            if (crsp == null)
            {
                return NotFound();
            }
            return View(crsp);
        }
        [HttpPost]
        public IActionResult Edit(CoursePrerequisite coursePrerequisite)
        {
            if (ModelState.IsValid)
            {
                var oldcrsp = _db.CoursePrerequesites.FirstOrDefault(o => o.Uuid == coursePrerequisite.Uuid);
                if(oldcrsp == null)
                {
                    return NotFound();
                }
                oldcrsp.MinGradeRequired = coursePrerequisite.MinGradeRequired;
                oldcrsp.CourseId = coursePrerequisite.CourseId;
                oldcrsp.PrerequisiteCourseId = coursePrerequisite.PrerequisiteCourseId;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(coursePrerequisite);
        }
        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var crsp = _db.CoursePrerequesites.FirstOrDefault(c => c.Uuid == uuid);
            if (crsp == null)
            {
                return NotFound();
            }
            return View(crsp);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var crsp = _db.CoursePrerequesites.FirstOrDefault(c => c.Uuid == uuid);
            if (crsp == null)
            {
                return NotFound();
            }

            _db.CoursePrerequesites.Remove(crsp);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
