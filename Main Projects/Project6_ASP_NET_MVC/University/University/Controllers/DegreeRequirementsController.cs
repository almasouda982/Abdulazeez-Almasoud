using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Dtos;
using University.Models;
using University.Repositories;

namespace University.Controllers
{
    [Authorize]

    public class DegreeRequirementsController : Controller
    {
        private readonly IDegreeRequirementRepository _degreeRequirementRepository;

        public DegreeRequirementsController(IDegreeRequirementRepository degreeRequirementRepository)
        {
            _degreeRequirementRepository = degreeRequirementRepository;
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
            var degreeRequirements = _degreeRequirementRepository.GetAll();
            IEnumerable<DegreeRequirementDto> degreeRequirementsDtos = degreeRequirements.Select(d => new DegreeRequirementDto
            {
                Id = d.Id,
                Uuid = d.Uuid,
                Name = d.DegreeProgram != null ? d.DegreeProgram.Name : "",
                CategoryName = d.CategoryName,
                RequiredCredits = d.RequiredCredits,
                MinLevel = d.MinLevel

            }).ToList();
            return View(degreeRequirementsDtos);
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
                _degreeRequirementRepository.AddDegreeRequirement(degreeRequirement);
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
            var degr = _degreeRequirementRepository.GetDegreeRequirementByUuid(uuid);
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
                var olddegr = _degreeRequirementRepository.GetDegreeRequirementByUuid(degreeRequirementUpdateDto.Uuid);
                if (olddegr == null)
                {
                    return NotFound();
                }

                olddegr.DegreeProgramId = degreeRequirementUpdateDto.DegreeProgramId;
                olddegr.CategoryName = degreeRequirementUpdateDto.CategoryName;
                olddegr.RequiredCredits = degreeRequirementUpdateDto.RequiredCredits;
                olddegr.MinLevel= degreeRequirementUpdateDto.MinLevel;

                _degreeRequirementRepository.UpdateDegreeRequirement(olddegr);
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
            var degr = _degreeRequirementRepository.GetDegreeRequirementByUuid(uuid);
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
            var degr = _degreeRequirementRepository.GetDegreeRequirementByUuid(uuid);
            if (degr == null)
            {
                return NotFound();
            }

            _degreeRequirementRepository.DeleteDegreeRequirement(uuid);
            return RedirectToAction("Index");
        }
        public void LoadDegreeRequirements()
        {
            var degreePrograms = _degreeRequirementRepository.GetDegreePrograms();
            SelectList selectListItems = new SelectList(degreePrograms, "Id", "Name");
            ViewBag.DegreePrograms = selectListItems;
        }
    }
}
