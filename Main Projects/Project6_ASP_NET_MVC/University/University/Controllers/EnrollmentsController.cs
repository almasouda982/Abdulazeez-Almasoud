using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly AppDbContext _db;

        public EnrollmentsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Enrollment> enrollments = _db.Enrollments.ToList();
            return View(enrollments);
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
        public IActionResult Create(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(enrollment);
            }
            _db.Enrollments.Add(enrollment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var enr = _db.Enrollments.Find(Id);
            if (enr == null)
            {
                return NotFound();
            }
            return View(enr);
        }
        [HttpPost]
        public IActionResult Edit(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _db.Enrollments.Update(enrollment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(enrollment);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var enr = _db.Enrollments.Find(Id);
            if (enr == null)
            {
                return NotFound();
            }
            return View(enr);
        }
        [HttpPost]
        public IActionResult Delete(Enrollment enrollment)
        {
            _db.Enrollments.Remove(enrollment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
