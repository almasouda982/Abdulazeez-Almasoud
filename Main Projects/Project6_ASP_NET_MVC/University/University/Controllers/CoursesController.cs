using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index()
        {
            var coursesDtos = _courseService.GetAllCourses();
            return View(coursesDtos);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseCreateDto courseCreateDto)
        {
            if (ModelState.IsValid)
            {
                _courseService.AddCourse(courseCreateDto);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill in all required fields.");
            return View(courseCreateDto);
        }

        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var crs = _courseService.GetCourseByUuid(uuid);
            if (crs == null) return NotFound();
            return View(crs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var crs = _courseService.GetCourseByUuid(uuid);
            if (crs == null) return NotFound();
            return View(crs);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(string uuid)
        {
            var crs = _courseService.GetCourseByUuid(uuid);
            if (crs == null) return NotFound();

            _courseService.DeleteCourse(uuid);
            return RedirectToAction("Index");
        }
    }
}