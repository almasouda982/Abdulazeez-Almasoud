using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Dtos;
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
            IEnumerable<DegreeProgramDto> degreeProgramDtos = _db.DegreePrograms.Select(dp => new DegreeProgramDto
            {
                Id = dp.Id,
                Uuid = dp.Uuid,
                Name = dp.Name,
                CatalogYear = dp.CatalogYear,
                TotalCreditsRequired = dp.TotalCreditsRequired
            })
                .ToList();
                return View(degreeProgramDtos);
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
                var degreeProgram = new DegreeProgram
                {
                    Name = degreeProgramCreateDto.Name,
                    CatalogYear = degreeProgramCreateDto.CatalogYear,
                    TotalCreditsRequired = degreeProgramCreateDto.TotalCreditsRequired
                };
                _db.DegreePrograms.Add(degreeProgram);
                _db.SaveChanges();
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
            var degp = _db.DegreePrograms.FirstOrDefault(d => d.Uuid == uuid);
            if (degp == null)
            {
                return NotFound();
            }
            var dto = new DegreeProgramUpdateDto
            {
                Uuid = degp.Uuid,
                Name = degp.Name,
                CatalogYear = degp.CatalogYear,
                TotalCreditsRequired = degp.TotalCreditsRequired
            };
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit(DegreeProgramUpdateDto degreeProgramUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var olddegp = _db.DegreePrograms.FirstOrDefault(o => o.Uuid == degreeProgramUpdateDto.Uuid);
                if(olddegp == null)
                {
                    return NotFound();
                }

                olddegp.Name = degreeProgramUpdateDto.Name;
                olddegp.CatalogYear = degreeProgramUpdateDto.CatalogYear;
                olddegp.TotalCreditsRequired = degreeProgramUpdateDto.TotalCreditsRequired;

                _db.SaveChanges();
                return RedirectToAction("Index");
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
            var degp = _db.DegreePrograms.FirstOrDefault(d => d.Uuid ==uuid);
            if (degp == null)
            {
                return NotFound();
            }
            var dto = new DegreeProgramUpdateDto
            {
                Uuid = degp.Uuid,
                Name = degp.Name,
                CatalogYear = degp.CatalogYear,
                TotalCreditsRequired = degp.TotalCreditsRequired
            };
            return View(dto);
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
        // null for now
        public void LoadDegreePrograms()
        {
        }
    }
}
