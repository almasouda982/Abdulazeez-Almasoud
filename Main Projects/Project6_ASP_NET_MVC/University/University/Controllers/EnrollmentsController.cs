using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using University.Data;
using University.Dtos;
using University.Models;
using University.Repositories;

namespace University.Controllers
{
    [Authorize]
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentsController(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        //public IActionResult Index()
        //{
        //    IEnumerable<Enrollment> enrollments = _db.Enrollments.ToList();
        //    return View(enrollments);
        //}

        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<Enrollment> enrollments = await _db.Enrollments
        //        .Include(e => e.Student)
        //        .Include(e => e.CourseSection).ToListAsync();

        //    return View(enrollments);
        //}

        public IActionResult Index()
        {
            var enrollments = _enrollmentRepository.GetAll();
            IEnumerable<EnrollmentDto> enrollmentsDto = enrollments.Select(e => new EnrollmentDto
            {
                Id = e.Id,
                Uuid = e.Uuid,
                LastName = e.Student != null ? e.Student.LastName : null,
                SectionNumber = e.CourseSection != null ? e.CourseSection.SectionNumber : (int?)null,
                FinalGrade = e.FinalGrade,
                LetterGradePoints = e.LetterGradePoints,
                Status = e.Status
            }).ToList();
            return View(enrollmentsDto);
        }


        //==============================
        // Create
        //==============================
       
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Create()
        {
            LoadEnrollment();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EnrollmentCreateDto enrollmentCreateDto)
        {
            if (ModelState.IsValid)
            {
                var enrollment = new Enrollment
                {
                    StudentId = enrollmentCreateDto.StudentId,
                    CourseSectionId = enrollmentCreateDto.CourseSectionId,
                    FinalGrade = enrollmentCreateDto.FinalGrade,
                    LetterGradePoints = enrollmentCreateDto.LetterGradePoints,
                    Status = enrollmentCreateDto.Status
                };
                _enrollmentRepository.AddEnrollment(enrollment);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill in all required fields.");
            LoadEnrollment();
            return View(enrollmentCreateDto);
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var enr = _enrollmentRepository.GetEnrollmentByUuid(uuid);
            if (enr == null)
            {
                return NotFound();
            }
            var dto = new EnrollmentUpdateDto
            {
                Uuid = enr.Uuid,
                StudentId = enr.StudentId,
                CourseSectionId = enr.CourseSectionId,
                FinalGrade = enr.FinalGrade,
                LetterGradePoints = enr.LetterGradePoints,
                Status = enr.Status
            };
            LoadEnrollment();

            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit(EnrollmentUpdateDto enrollmentUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var oldenr = _enrollmentRepository.GetEnrollmentByUuid(enrollmentUpdateDto.Uuid);
                if(oldenr == null)
                {
                    return NotFound();
                }

                //replaces update
                oldenr.StudentId = enrollmentUpdateDto.StudentId;
                oldenr.CourseSectionId = enrollmentUpdateDto.CourseSectionId;
                oldenr.FinalGrade = enrollmentUpdateDto.FinalGrade;
                oldenr.LetterGradePoints = enrollmentUpdateDto.LetterGradePoints;
                oldenr.Status = enrollmentUpdateDto.Status;

                _enrollmentRepository.UpdateEnrollment(oldenr);
                return RedirectToAction("Index");
    }

            ModelState.AddModelError("", "Please fill all the required fields.");
            LoadEnrollment();
            return View(enrollmentUpdateDto);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var enr = _enrollmentRepository.GetEnrollmentByUuid(uuid);
            if (enr == null)
            {
                return NotFound();
            }
            var dto = new EnrollmentUpdateDto
            {
                Uuid = enr.Uuid,
                StudentId = enr.StudentId,
                CourseSectionId = enr.CourseSectionId,
                FinalGrade = enr.FinalGrade,
                LetterGradePoints = enr.LetterGradePoints,
                Status = enr.Status
            };
            return View(dto);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var enr = _enrollmentRepository.GetEnrollmentByUuid(uuid);
            if (enr == null)
            {
                return NotFound();
            }

            _enrollmentRepository.DeleteEnrollment(uuid);
            return RedirectToAction("Index");
        }

        public void LoadEnrollment()
        {
            var students = _enrollmentRepository.GetStudents();
            SelectList selectListItems1 = new SelectList(students, "Id", "LastName");
            ViewBag.Students = selectListItems1;
            var courseSections = _enrollmentRepository.GetCourseSections();
            SelectList selectListItems2 = new SelectList(courseSections, "Id", "SectionNumber");
            ViewBag.CourseSections = selectListItems2;
        }
    }
}
