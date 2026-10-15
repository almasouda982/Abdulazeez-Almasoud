using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Dtos;
using University.Models;
using University.Repositories;
using University.Services.Base;

namespace University.Controllers
{
    [Authorize]

    public class DegreeRequirementsController : Controller
    {
        private readonly IDegreeRequirementService _degreeRequirementService;

        public DegreeRequirementsController(IDegreeRequirementService degreeRequirementService)
        {
            _degreeRequirementService = degreeRequirementService;
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
            var degreeRequirementsDtos = _degreeRequirementService.GetAllDegreeRequirements();

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

                _degreeRequirementService.AddDegreeRequirement(degreeRequirementCreateDto);
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
            var degr = _degreeRequirementService.GetDegreeRequirementByUuid(uuid);
            if (degr == null)
            {
                return NotFound();
            }

            LoadDegreeRequirements();
            return View(degr);
        }
        [HttpPost]
        public IActionResult Edit(DegreeRequirementUpdateDto  degreeRequirementUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _degreeRequirementService.UpdateDegreeRequirement(degreeRequirementUpdateDto);
                    return RedirectToAction("Index");
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
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
            var degr = _degreeRequirementService.GetDegreeRequirementByUuid(uuid);
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
            var degr = _degreeRequirementService.GetDegreeRequirementByUuid(uuid);
            if (degr == null)
            {
                return NotFound();
            }

            _degreeRequirementService.DeleteDegreeRequirement(uuid);
            return RedirectToAction("Index");
        }
        public void LoadDegreeRequirements()
        {
            var degreePrograms = _degreeRequirementService.GetDegreePrograms();
            SelectList selectListItems = new SelectList(degreePrograms, "Id", "Name");
            ViewBag.DegreePrograms = selectListItems;
        }
    }
}
