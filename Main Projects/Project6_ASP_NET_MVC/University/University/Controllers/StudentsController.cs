using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _db;

        public StudentsController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Student> students = _db.Students.ToList();
            return View(students);
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
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(student);
            }
            
            _db.Students.Add(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(long Id)
        {
            var std = _db.Students.Find(Id);
            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(student);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(long Id)
        {
            var std = _db.Students.Find(Id);
            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        public IActionResult Delete(Student student)
        {
            _db.Students.Remove(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
