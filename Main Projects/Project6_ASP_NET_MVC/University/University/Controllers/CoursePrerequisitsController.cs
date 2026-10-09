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

    public class CoursePrerequisitsController : Controller
    {
        private readonly ICoursePrerequisiteRepository _coursePrerequisiteRepository;

        public CoursePrerequisitsController(ICoursePrerequisiteRepository coursePrerequisiteRepository)
        {
            _coursePrerequisiteRepository = coursePrerequisiteRepository;
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
            var coursePrerequisites = _coursePrerequisiteRepository.GetAll();
            IEnumerable<CoursePrerequisiteDto> coursePrerequisiteDtos = coursePrerequisites.Select(c => new CoursePrerequisiteDto
            {
                Id = c.Id,
                Uuid = c.Uuid,
                MinGradeRequired = c.MinGradeRequired,
                Code = c.Course != null ? c.Course.Code : null,
                PrerequisiteCourseId = c.PrerequisiteCourseId

            }).ToList();
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
                var coursePrerequisite = new CoursePrerequisite
                {
                    MinGradeRequired = coursePrerequisiteCreateDto.MinGradeRequired,
                    CourseId = coursePrerequisiteCreateDto.CourseId,
                    PrerequisiteCourseId = coursePrerequisiteCreateDto.PrerequisiteCourseId
                };

                _coursePrerequisiteRepository.AddCoursePrerequisite(coursePrerequisite);
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
            var crsp = _coursePrerequisiteRepository.GetCoursePrerequisiteByUuid(uuid);
            if (crsp == null)
            {
                return NotFound();
            }
            var dto = new CoursePrerequisiteUpdateDto
            {
                Uuid = crsp.Uuid,
                MinGradeRequired = crsp.MinGradeRequired,
                CourseId = crsp.CourseId,
                PrerequisiteCourseId = crsp.PrerequisiteCourseId
            };
            LoadCourses();
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit(CoursePrerequisiteUpdateDto coursePrerequisiteUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var oldcrsp = _coursePrerequisiteRepository.GetCoursePrerequisiteByUuid(coursePrerequisiteUpdateDto.Uuid);
                if(oldcrsp == null)
                {
                    return NotFound();
                }
                oldcrsp.MinGradeRequired = coursePrerequisiteUpdateDto.MinGradeRequired;
                oldcrsp.CourseId = coursePrerequisiteUpdateDto.CourseId;
                oldcrsp.PrerequisiteCourseId = coursePrerequisiteUpdateDto.PrerequisiteCourseId;

                _coursePrerequisiteRepository.UpdateCoursePrerequisite(oldcrsp);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(coursePrerequisiteUpdateDto);
        }
        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var crsp = _coursePrerequisiteRepository.GetCoursePrerequisiteByUuid(uuid);
            if (crsp == null)
            {
                return NotFound();
            }
            var dto = new CoursePrerequisiteUpdateDto
            {
                Uuid = crsp.Uuid,
                MinGradeRequired = crsp.MinGradeRequired,
                CourseId = crsp.CourseId,
                PrerequisiteCourseId = crsp.PrerequisiteCourseId
            };
            return View(dto);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var crsp = _coursePrerequisiteRepository.GetCoursePrerequisiteByUuid(uuid);
            if (crsp == null)
            {
                return NotFound();
            }

            _coursePrerequisiteRepository.DeleteCoursePrerequisite(uuid);
            return RedirectToAction("Index");
        }
        public void LoadCourses()
        {
            var courses = _coursePrerequisiteRepository.GetCourses();
            SelectList selectListItems = new SelectList(courses, "Id", "Code");
            ViewBag.Courses = selectListItems;
        }
    }
}
