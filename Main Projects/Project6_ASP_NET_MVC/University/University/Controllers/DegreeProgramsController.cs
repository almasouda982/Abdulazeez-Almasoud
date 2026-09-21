using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
    [Authorize]

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
        public IActionResult Edit(string uuid)
        {
            var degp = _db.DegreePrograms.FirstOrDefault(d => d.Uuid == uuid);
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
                var olddegp = _db.DegreePrograms.FirstOrDefault(o => o.Uuid == degreeProgram.Uuid);
                if(olddegp == null)
                {
                    return NotFound();
                }

                olddegp.Name = degreeProgram.Name;
                olddegp.CatalogYear = degreeProgram.CatalogYear;
                olddegp.TotalCreditsRequired = degreeProgram.TotalCreditsRequired;

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
        public IActionResult Delete(string uuid)
        {
            var degp = _db.DegreePrograms.FirstOrDefault(d => d.Uuid ==uuid);
            if (degp == null)
            {
                return NotFound();
            }
            return View(degp);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var degp = _db.DegreePrograms.FirstOrDefault(d => d.Uuid==uuid);
            if (degp == null)
            {
                return NotFound();
            }
            _db.DegreePrograms.Remove(degp);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
