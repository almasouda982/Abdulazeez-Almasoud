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

namespace University.Controllers
{
    [Authorize]
    public class EnrollmentsController : Controller
    {
        private readonly AppDbContext _db;

        public EnrollmentsController(AppDbContext db)
        {
            _db = db;
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
            IEnumerable<EnrollmentDto> enrollments = _db.Enrollments.Select(e => new EnrollmentDto
            {
                Id = e.Id,
                Uuid = e.Uuid,
                LastName = e.Student != null ? e.Student.LastName : null,
                SectionNumber = e.CourseSection != null ? e.CourseSection.SectionNumber : (int?)null,
                FinalGrade = e.FinalGrade,
                LetterGradePoints = e.LetterGradePoints,
                Status = e.Status
            }).ToList();
            return View(enrollments);
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
                _db.Enrollments.Add(enrollment);
                _db.SaveChanges();
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
            var enr = _db.Enrollments.FirstOrDefault(e => e.Uuid == uuid);
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
                var oldenr = _db.Enrollments.FirstOrDefault(e => e.Uuid == enrollmentUpdateDto.Uuid);
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

                _db.SaveChanges();
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
            var enr = _db.Enrollments.FirstOrDefault(e => e.Uuid == uuid);
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
            var enr = _db.Enrollments.FirstOrDefault(e => e.Uuid == uuid);
            if (enr == null)
            {
                return NotFound();
            }

            _db.Enrollments.Remove(enr);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void LoadEnrollment()
        {
            var students = _db.Students.ToList();
            SelectList selectListItems1 = new SelectList(students, "Id", "LastName");
            ViewBag.Students = selectListItems1;
            var courseSections = _db.CourseSections.ToList();
            SelectList selectListItems2 = new SelectList(courseSections, "Id", "SectionNumber");
            ViewBag.CourseSections = selectListItems2;
        }
    }
}
