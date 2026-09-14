using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
    public class DegreeProgramsController : Controller
    {
        private readonly AppDbContext _db;

        public DegreeProgramsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<DegreeProgram> degreePrograms = _db.DegreePrograms.ToList();
            return View(degreePrograms);
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
        public IActionResult Create(DegreeProgram degreeProgram)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(degreeProgram);
            }
            _db.DegreePrograms.Add(degreeProgram);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var degp = _db.DegreePrograms.Find(Id);
            if (degp == null)
            {
                return NotFound();
            }
            return View(degp);
        }
        [HttpPost]
        public IActionResult Edit(DegreeProgram degreeProgram)
        {
            if (ModelState.IsValid)
            {
                _db.DegreePrograms.Update(degreeProgram);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(degreeProgram);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var degp = _db.DegreePrograms.Find(Id);
            if (degp == null)
            {
                return NotFound();
            }
            return View(degp);
        }
        [HttpPost]
        public IActionResult Delete(DegreeProgram degreeProgram)
        {
            _db.DegreePrograms.Remove(degreeProgram);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
