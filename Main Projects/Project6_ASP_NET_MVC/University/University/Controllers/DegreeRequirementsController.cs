using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Controllers
{
    [Authorize]

    public class DegreeRequirementsController : Controller
    {
        private readonly AppDbContext _db;

        public DegreeRequirementsController(AppDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    IEnumerable<DegreeRequirement> degreeRequirements = _db.DegreeRequirements.ToList();
        //    return View(degreeRequirements);
        //}
        public async Task<IActionResult> Index()
        {
            IEnumerable<DegreeRequirement> degreeRequirements = await _db.DegreeRequirements
                .Include(d => d.DegreeProgram).ToListAsync();
            return View(degreeRequirements);
        }

        //==============================
        // Create
        //==============================
        [HttpGet]
        public IActionResult Create()
        {
            var degreePrograms = _db.DegreePrograms.ToList();
            SelectList selectListItems = new SelectList(degreePrograms, "Id", "Name");
            ViewBag.DegreePrograms = selectListItems;
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
        public IActionResult Edit(string uuid)
        {
            var degr = _db.DegreeRequirements.FirstOrDefault(d => d.Uuid == uuid);
            if (degr == null)
            {
                return NotFound();
            }
            var degreePrograms = _db.DegreePrograms.ToList();
            SelectList selectListItems = new SelectList(degreePrograms, "Id", "Name");
            ViewBag.DegreePrograms = selectListItems;
            return View(degr);
        }
        [HttpPost]
        public IActionResult Edit(DegreeRequirement degreeRequirement)
        {
            if (ModelState.IsValid)
            {
                var olddegr = _db.DegreeRequirements.FirstOrDefault(o => o.Uuid == degreeRequirement.Uuid);
                if (olddegr == null)
                {
                    return NotFound();
                }

                olddegr.DegreeProgramId = degreeRequirement.DegreeProgramId;
                olddegr.CategoryName = degreeRequirement.CategoryName;
                olddegr.RequiredCredits = degreeRequirement.RequiredCredits;
                olddegr.MinLevel= degreeRequirement.MinLevel;


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
        public IActionResult Delete(string uuid)
        {
            var degr = _db.DegreeRequirements.FirstOrDefault(d => d.Uuid == uuid);
            if (degr == null)
            {
                return NotFound();
            }
            return View(degr);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var degr = _db.DegreeRequirements.FirstOrDefault(d => d.Uuid ==uuid);
            if (degr == null)
            {
                return NotFound();
            }

            _db.DegreeRequirements.Remove(degr);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
