using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Application.Dtos;
using University.Application.Services.Base;

namespace University.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        //public IActionResult Index()
        //{
        //    IEnumerable<Course> courses = _db.Courses.ToList();
        //    return View(courses);
        //}


        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<Course> courses = await _db.Courses

        //        .Include(cp => cp.CoursePrerequisites)
        //        .Include(d => d.CourseSections).ToListAsync();

        //    return View(courses);
        //}


        public IActionResult Index()
        {
            var coursesDtos = _courseService.GetAllCourses();
            return View(coursesDtos);
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
        public IActionResult Create(CourseCreateDto courseCreateDto)
        {
            if(ModelState.IsValid)
            {

                _courseService.AddCourse(courseCreateDto);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill in all required fields.");
            return View(courseCreateDto);
        
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var crs = _courseService.GetCourseByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }

            return View(crs);
        }
        [HttpPost]
        public IActionResult Edit(CourseUpdateDto courseUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _courseService.UpdateCourse(courseUpdateDto);
                    return RedirectToAction("Index");
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(courseUpdateDto);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var crs = _courseService.GetCourseByUuid(uuid);
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
            var crs = _courseService.GetCourseByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }

            _courseService.DeleteCourse(uuid);
            return RedirectToAction("Index");
        }
        // no viewbags so far
        //public void LoadCourses()
        //{
        //    var courses = _db.Courses.ToList();
        //    ViewBag.Courses = courses;
        //}

    }
}
