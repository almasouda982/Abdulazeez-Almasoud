using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
    public class DegreeRequirementsController : Controller
    {
        private readonly AppDbContext _db;

        public DegreeRequirementsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<DegreeRequirement> degreeRequirements = _db.DegreeRequirements.ToList();
            return View(degreeRequirements);
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
        public IActionResult Create(DegreeRequirement degreeRequirement)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(degreeRequirement);
            }
            _db.DegreeRequirements.Add(degreeRequirement);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var degr = _db.DegreeRequirements.Find(Id);
            if (degr == null)
            {
                return NotFound();
            }
            return View(degr);
        }
        [HttpPost]
        public IActionResult Edit(DegreeRequirement degreeRequirement)
        {
            if (ModelState.IsValid)
            {
                _db.DegreeRequirements.Update(degreeRequirement);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(degreeRequirement);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var degr = _db.DegreeRequirements.Find(Id);
            if (degr == null)
            {
                return NotFound();
            }
            return View(degr);
        }
        [HttpPost]
        public IActionResult Delete(DegreeRequirement degreeRequirement)
        {
            _db.DegreeRequirements.Remove(degreeRequirement);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
