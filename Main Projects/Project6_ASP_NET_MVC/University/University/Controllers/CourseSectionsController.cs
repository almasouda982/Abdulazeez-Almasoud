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

    public class CourseSectionsController : Controller
    {
        private readonly ICourseSectionRepository _courseSectionRepository;

        public CourseSectionsController(ICourseSectionRepository courseSectionRepository)
        {
            _courseSectionRepository = courseSectionRepository;
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
            var courseSections = _courseSectionRepository.GetAll();
            IEnumerable<CourseSectionDto> courseSectionDtos = courseSections.Select(cs => new CourseSectionDto
            {
                Id = cs.Id,
                Uuid = cs.Uuid,
                Code = cs.Course != null ? cs.Course.Code : null,
                Semester = cs.Semester,
                SectionNumber = cs.SectionNumber,
                MaxCapacity = cs.MaxCapacity,
                DaysOfWeek = cs.DaysOfWeek,
                TimeSlot = cs.TimeSlot
            }).ToList();
            return View(courseSectionDtos);
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
                var courseSection = new CourseSection
                {
                    CourseId = courseSectionDto.CourseId,
                    Semester = courseSectionDto.Semester,
                    SectionNumber = courseSectionDto.SectionNumber,
                    MaxCapacity = courseSectionDto.MaxCapacity,
                    DaysOfWeek = courseSectionDto.DaysOfWeek,
                    TimeSlot = courseSectionDto.TimeSlot
                };
                _courseSectionRepository.AddCourseSection(courseSection);
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
            var crs = _courseSectionRepository.GetCourseSectionByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }
            var dto = new CourseSectionUpdateDto
            {
                Uuid = crs.Uuid,
                CourseId = crs.CourseId,
                Semester = crs.Semester,
                SectionNumber = crs.SectionNumber,
                MaxCapacity = crs.MaxCapacity,
                DaysOfWeek = crs.DaysOfWeek,
                TimeSlot = crs.TimeSlot
            };
            LoadCourseSections();
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit(CourseSectionUpdateDto courseSectionUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var oldcrs = _courseSectionRepository.GetCourseSectionByUuid(courseSectionUpdateDto.Uuid);
                if (oldcrs == null)
                {
                    return NotFound();
                }

                oldcrs.CourseId = courseSectionUpdateDto.CourseId;
                oldcrs.Semester = courseSectionUpdateDto.Semester;
                oldcrs.SectionNumber = courseSectionUpdateDto.SectionNumber;
                oldcrs.MaxCapacity = courseSectionUpdateDto.MaxCapacity;
                oldcrs.DaysOfWeek = courseSectionUpdateDto.DaysOfWeek;
                oldcrs.TimeSlot = courseSectionUpdateDto.TimeSlot;

                _courseSectionRepository.UpdateCourseSection(oldcrs);
                return RedirectToAction("Index");
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
            var crs = _courseSectionRepository.GetCourseSectionByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }
            var dto = new CourseSectionUpdateDto
            {
                Uuid = crs.Uuid,
                CourseId = crs.CourseId,
                Semester = crs.Semester,
                SectionNumber = crs.SectionNumber,
                MaxCapacity = crs.MaxCapacity,
                DaysOfWeek = crs.DaysOfWeek,
                TimeSlot = crs.TimeSlot
            };
            return View(dto);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var crs = _courseSectionRepository.GetCourseSectionByUuid(uuid);
            if (crs == null)
            {
                return NotFound();
            }
            _courseSectionRepository.DeleteCourseSection(uuid);
            return RedirectToAction("Index");
        }
        public void LoadCourseSections()
        {
            var courses = _courseSectionRepository.GetCourses();
            SelectList selectListItems = new SelectList(courses, "Id", "Code");
            ViewBag.Courses = selectListItems;
        }
    }
}
