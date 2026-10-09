using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Dtos;
using University.Models;
using University.Repositories;

namespace University.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
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
            var courses = _courseRepository.GetAll();
            IEnumerable<CourseDto> courseDtos = courses.Select(c => new CourseDto
                {
                    Id = c.Id,
                    Uuid = c.Uuid,
                    Code = c.Code,
                    Title = c.Title,
                    Credits = c.Credits,
                    Description = c.Description
                })
                .ToList();
                return View(courseDtos);
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
                var course = new Course
                {
                    Code = courseCreateDto.Code,
                    Title = courseCreateDto.Title,
                    Credits = courseCreateDto.Credits,
                    Description = courseCreateDto.Description
                };

                _courseRepository.AddCourse(course);
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
            var crs = _courseRepository.GetCourseByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }
            var dto = new CourseUpdateDto
            {
                Uuid = crs.Uuid,
                Code = crs.Code,
                Title = crs.Title,
                Credits = crs.Credits,
                Description = crs.Description
            };
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit(CourseUpdateDto courseUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var oldcrs = _courseRepository.GetCourseByUuid(courseUpdateDto.Uuid);
                if (oldcrs == null)
                {
                    return NotFound();
                }

                oldcrs.Code = courseUpdateDto.Code;
                oldcrs.Title = courseUpdateDto.Title;
                oldcrs.Credits = courseUpdateDto.Credits;
                oldcrs.Description= courseUpdateDto.Description;

                _courseRepository.UpdateCourse(oldcrs);
                return RedirectToAction("Index");
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
            var crs = _courseRepository.GetCourseByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }
            var dto = new CourseUpdateDto
            {
                Uuid = crs.Uuid,
                Code = crs.Code,
                Title = crs.Title,
                Credits = crs.Credits,
                Description = crs.Description
            };
            return View(dto);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var crs = _courseRepository.GetCourseByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }

            _courseRepository.DeleteCourse(uuid);
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
