using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Dtos;
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
        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<DegreeRequirement> degreeRequirements = await _db.DegreeRequirements
        //        .Include(d => d.DegreeProgram).ToListAsync();
        //    return View(degreeRequirements);
        //}

        public IActionResult Index()
        {
            IEnumerable<DegreeRequirementDto> degreeRequirementDtos = _db.DegreeRequirements.Select(d => new DegreeRequirementDto
            {
                Id = d.Id,
                Uuid = d.Uuid,
                Name = d.DegreeProgram != null ? d.DegreeProgram.Name : "",
                CategoryName = d.CategoryName,
                RequiredCredits = d.RequiredCredits,
                MinLevel = d.MinLevel

            }).ToList();
            return View(degreeRequirementDtos);
        }

        //==============================
        // Create
        //==============================
        [HttpGet]
        public IActionResult Create()
        {

            LoadDegreeRequirements();
            return View();
        }
        [HttpPost]
        public IActionResult Create(DegreeRequirementCreateDto degreeRequirementCreateDto)
        {
            if (ModelState.IsValid)
            {
                var degreeRequirement = new DegreeRequirement
                {
                    DegreeProgramId = degreeRequirementCreateDto.DegreeProgramId,
                    CategoryName = degreeRequirementCreateDto.CategoryName,
                    RequiredCredits = degreeRequirementCreateDto.RequiredCredits,
                    MinLevel = degreeRequirementCreateDto.MinLevel
                };
                _db.DegreeRequirements.Add(degreeRequirement);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(degreeRequirementCreateDto);
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
            var dto = new DegreeRequirementUpdateDto
            {
                Uuid = degr.Uuid,
                DegreeProgramId = degr.DegreeProgramId,
                CategoryName = degr.CategoryName,
                RequiredCredits = degr.RequiredCredits,
                MinLevel = degr.MinLevel
            };

            LoadDegreeRequirements();
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit(DegreeRequirementUpdateDto  degreeRequirementUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var olddegr = _db.DegreeRequirements.FirstOrDefault(o => o.Uuid == degreeRequirementUpdateDto.Uuid);
                if (olddegr == null)
                {
                    return NotFound();
                }

                olddegr.DegreeProgramId = degreeRequirementUpdateDto.DegreeProgramId;
                olddegr.CategoryName = degreeRequirementUpdateDto.CategoryName;
                olddegr.RequiredCredits = degreeRequirementUpdateDto.RequiredCredits;
                olddegr.MinLevel= degreeRequirementUpdateDto.MinLevel;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            LoadDegreeRequirements();
            return View(degreeRequirementUpdateDto);
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
            var dto = new DegreeRequirementUpdateDto
            {
                Uuid = degr.Uuid,
                DegreeProgramId = degr.DegreeProgramId,
                CategoryName = degr.CategoryName,
                RequiredCredits = degr.RequiredCredits,
                MinLevel = degr.MinLevel
            };
            return View(dto);
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
        public void LoadDegreeRequirements()
        {
            var degreePrograms = _db.DegreePrograms.ToList();
            SelectList selectListItems = new SelectList(degreePrograms, "Id", "Name");
            ViewBag.DegreePrograms = selectListItems;
        }
    }
}
