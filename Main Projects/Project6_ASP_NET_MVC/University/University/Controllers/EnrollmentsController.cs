using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using University.Data;
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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Enrollment> enrollments = await _db.Enrollments
                .Include(e => e.Student)
                .Include(e => e.CourseSection).ToListAsync();

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
            var students = _db.Students.ToList();
            SelectList selectListItems1 = new SelectList(students, "Id", "LastName");
            ViewBag.Students = selectListItems1;
            var courseSections = _db.CourseSections.ToList();
            SelectList selectListItems2 = new SelectList(courseSections, "Id", "SectionNumber");
            ViewBag.CourseSections = selectListItems2;

            return View();
        }
        [HttpPost]
        public IActionResult Create(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(enrollment);
            }
            _db.Enrollments.Add(enrollment);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
            var students = _db.Students.ToList();
            SelectList selectListItems1 = new SelectList(students, "Id", "LastName");
            ViewBag.Students = selectListItems1;
            var courseSections = _db.CourseSections.ToList();
            SelectList selectListItems2 = new SelectList(courseSections, "Id", "SectionNumber");
            ViewBag.CourseSections = selectListItems2;

            return View(enr);
        }
        [HttpPost]
        public IActionResult Edit(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                var oldenr = _db.Enrollments.FirstOrDefault(e => e.Uuid == enrollment.Uuid);
                if(oldenr == null)
                {
                    return NotFound();
                }

                //replaces update
                oldenr.StudentId = enrollment.StudentId;
                oldenr.CourseSectionId = enrollment.CourseSectionId;
                oldenr.FinalGrade = enrollment.FinalGrade;
                oldenr.LetterGradePoints = enrollment.LetterGradePoints;
                oldenr.Status = enrollment.Status;

                _db.SaveChanges();
                return RedirectToAction("Index");
    }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(enrollment);
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
            return View(enr);
        }
        [HttpPost]
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
    }
}
