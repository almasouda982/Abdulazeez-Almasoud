using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Dtos;
using University.Models;

namespace University.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _db;

        public CoursesController(AppDbContext db)
        {
            _db = db;
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
            IEnumerable<CourseDto> courseDtos = _db.Courses.Select(c => new CourseDto
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
                _db.Courses.Add(course);
                _db.SaveChanges();
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
            var crs = _db.Courses.FirstOrDefault(c => c.Uuid == uuid);
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
                var oldcrs = _db.Courses.FirstOrDefault(o => o.Uuid == courseUpdateDto.Uuid);
                if (oldcrs == null)
                {
                    return NotFound();
                }

                oldcrs.Code = courseUpdateDto.Code;
                oldcrs.Title = courseUpdateDto.Title;
                oldcrs.Credits = courseUpdateDto.Credits;
                oldcrs.Description= courseUpdateDto.Description;

                _db.SaveChanges();
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
            var crs = _db.Courses.FirstOrDefault(c => c.Uuid ==uuid);
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
            var crs = _db.Courses.FirstOrDefault(c => c.Uuid == uuid);
            if (crs == null)
            {
                return NotFound();
            }

            _db.Courses.Remove(crs);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        // no viewbags so far
        public void LoadCourses()
        {
            var courses = _db.Courses.ToList();
            ViewBag.Courses = courses;
        }

    }
}
