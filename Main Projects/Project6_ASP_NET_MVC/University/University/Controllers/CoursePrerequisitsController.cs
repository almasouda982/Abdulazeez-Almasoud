using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Dtos;
using University.Services.Base;

namespace University.Controllers
{
    [Authorize]

    public class CoursePrerequisitsController : Controller
    {
        //private readonly ICoursePrerequisiteRepository _coursePrerequisiteRepository;

        private readonly ICoursePrerequisiteService _coursePrerequisiteService;
        public CoursePrerequisitsController(ICoursePrerequisiteService coursePrerequisiteService)
        {
            _coursePrerequisiteService = coursePrerequisiteService;
        }
        //public IActionResult Index()
        //{
        //    IEnumerable<CoursePrerequisite> coursePrerequisites = _db.CoursePrerequisites.ToList();
        //    return View(coursePrerequisites);
        //}

        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<CoursePrerequisite> coursePrerequisites = await _db.CoursePrerequisites
        //        .Include(c => c.Course).ToListAsync();
        //    return View(coursePrerequisites);
        //}

        public IActionResult Index()
        {
            var coursePrerequisiteDtos = _coursePrerequisiteService.GetAllCoursePrerequisites();
            return View(coursePrerequisiteDtos);
        }
        //============
        //Create
        //============
        [HttpGet]
        public IActionResult Create()
        {
            LoadCourses();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CoursePrerequisiteCreateDto coursePrerequisiteCreateDto)
        {
            if (ModelState.IsValid)
            {


                _coursePrerequisiteService.AddCoursePrerequisite(coursePrerequisiteCreateDto);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill in all required fields.");
            LoadCourses();
            return View(coursePrerequisiteCreateDto);

        }

        //============
        //Edit
        //============
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var crsp = _coursePrerequisiteService.GetCoursePrerequisiteByUuid(uuid);
            if (crsp == null)
            {
                return NotFound();
            }

            LoadCourses();
            return View(crsp);
        }
        [HttpPost]
        public IActionResult Edit(CoursePrerequisiteUpdateDto coursePrerequisiteUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _coursePrerequisiteService.UpdateCoursePrerequisite(coursePrerequisiteUpdateDto);
                    return RedirectToAction("Index");
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            LoadCourses();
            return View(coursePrerequisiteUpdateDto);
        }
        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var crsp = _coursePrerequisiteService.GetCoursePrerequisiteByUuid(uuid);
            if (crsp == null)
            {
                return NotFound();
            }

            return View(crsp);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var crsp = _coursePrerequisiteService.GetCoursePrerequisiteByUuid(uuid);
            if (crsp == null)
            {
                return NotFound();
            }

            _coursePrerequisiteService.DeleteCoursePrerequisite(uuid);
            return RedirectToAction("Index");
        }
        public void LoadCourses()
        {
            var courses = _coursePrerequisiteService.GetCourses();
            SelectList selectListItems = new SelectList(courses, "Id", "Code");
            ViewBag.Courses = selectListItems;
        }
    }
}
