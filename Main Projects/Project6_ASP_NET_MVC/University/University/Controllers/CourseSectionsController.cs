using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Application.Dtos;
using University.Application.Services.Base;

namespace University.Controllers
{
    [Authorize]

    public class CourseSectionsController : Controller
    {
        private readonly ICourseSectionService _courseSectionService;

        public CourseSectionsController(ICourseSectionService courseSectionService)
        {
            _courseSectionService = courseSectionService;
        }
        //public IActionResult Index()
        //{
        //    IEnumerable<CourseSection> courseSections = _db.CourseSections.ToList();
        //    return View(courseSections);
        //}

        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<CourseSection> courseSections = await _db.CourseSections

        //        .Include(w => w.WaitlistEntries)
        //        .Include(e => e.Enrollments)
        //        .Include(c => c.Course)
        //        .ToListAsync();

        //    return View(courseSections);
        //}

        public IActionResult Index()
        {
            var courseSectionsDtos = _courseSectionService.GetAllCourseSections();

            return View(courseSectionsDtos);
        }


        //==============================
        // Create
        //==============================
        [HttpGet]
        public IActionResult Create()
        {
            LoadCourseSections();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CourseSectionCreateDto courseSectionDto)
        {
            if (ModelState.IsValid)
            {

                _courseSectionService.AddCourseSection(courseSectionDto);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill in all required fields.");
            LoadCourseSections();
            return View(courseSectionDto);
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var crs = _courseSectionService.GetCourseSectionByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }

            LoadCourseSections();
            return View(crs);
        }
        [HttpPost]
        public IActionResult Edit(CourseSectionUpdateDto courseSectionUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _courseSectionService.UpdateCourseSection(courseSectionUpdateDto);
                    return RedirectToAction("Index");
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            LoadCourseSections();
            return View(courseSectionUpdateDto);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var crs = _courseSectionService.GetCourseSectionByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }

            return View(crs);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var crs = _courseSectionService.GetCourseSectionByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }
            _courseSectionService.DeleteCourseSection(uuid);
            return RedirectToAction("Index");
        }
        public void LoadCourseSections()
        {
            var courses = _courseSectionService.GetCourses();
            SelectList selectListItems = new SelectList(courses, "Id", "Code");
            ViewBag.Courses = selectListItems;
        }
    }
}
