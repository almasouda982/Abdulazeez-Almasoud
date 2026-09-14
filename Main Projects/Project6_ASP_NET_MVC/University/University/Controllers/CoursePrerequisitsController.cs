using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
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
        public IActionResult Edit(int Id)
        {
            var crsp = _db.CoursePrerequesites.Find(Id);
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
                _db.CoursePrerequesites.Update(coursePrerequisite);
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
        public IActionResult Delete(int Id)
        {
            var crsp = _db.CoursePrerequesites.Find(Id);
            if (crsp == null)
            {
                return NotFound();
            }
            return View(crsp);
        }
        [HttpPost]
        public IActionResult Delete(CoursePrerequisite coursePrerequisite)
        {
            _db.CoursePrerequesites.Remove(coursePrerequisite);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
