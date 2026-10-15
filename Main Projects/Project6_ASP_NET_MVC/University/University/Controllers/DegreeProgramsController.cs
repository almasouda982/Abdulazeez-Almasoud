using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Application.Dtos;
using University.Application.Services.Base;

namespace University.Controllers
{
    [Authorize]

    public class DegreeProgramsController : Controller
    {
        private readonly IDegreeProgramService _degreeProgramService;

        public DegreeProgramsController(IDegreeProgramService degreeProgramService)
        {
            _degreeProgramService = degreeProgramService;
        }

        //public IActionResult Index()
        //{
        //    IEnumerable<DegreeProgram> degreePrograms = _db.DegreePrograms.ToList();
        //    return View(degreePrograms);
        //}

        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<DegreeProgram> degreePrograms = await _db.DegreePrograms

        //        .Include(s => s.Students)
        //        .Include(d => d.DegreeRequirements).ToListAsync();

        //    return View(degreePrograms);
        //}


        public IActionResult Index()
        {
            var degreeProgramsDtos = _degreeProgramService.GetAllDegreePrograms();
            return View(degreeProgramsDtos);
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
        public IActionResult Create(DegreeProgramCreateDto degreeProgramCreateDto)
        {
            if (ModelState.IsValid)
            {

                _degreeProgramService.AddDegreeProgram(degreeProgramCreateDto);
                return RedirectToAction("Index");
            }
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(degreeProgramCreateDto);

        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var degp = _degreeProgramService.GetDegreeProgramByUuid(uuid);
            if (degp == null)
            {
                return NotFound();
            }

            return View(degp);
        }
        [HttpPost]
        public IActionResult Edit(DegreeProgramUpdateDto degreeProgramUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _degreeProgramService.UpdateDegreeProgram(degreeProgramUpdateDto);
                    return RedirectToAction("Index");
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(degreeProgramUpdateDto);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var degp = _degreeProgramService.GetDegreeProgramByUuid(uuid);
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
            var degp = _degreeProgramService.GetDegreeProgramByUuid(uuid);
            if (degp == null)
            {
                return NotFound();
            }
            _degreeProgramService.DeleteDegreeProgram(uuid);
            return RedirectToAction("Index");
        }
        //// null for now
        //public void LoadDegreePrograms()
        //{
        //}
    }
}
