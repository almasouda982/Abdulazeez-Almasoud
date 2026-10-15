using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Application.Dtos;
using University.Application.Services.Base;


namespace University.Controllers
{
    [Authorize]
    public class EnrollmentsController : Controller
    {
        //private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IEnrollmentsService _enrollmentsService;
        public EnrollmentsController(IEnrollmentsService enrollmentsService)
        {
            _enrollmentsService = enrollmentsService;
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
            var enrollmentsDto = _enrollmentsService.GetAllEnrollments();
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
                _enrollmentsService.AddEnrollment(enrollmentCreateDto);
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
            var enr = _enrollmentsService.GetEnrollmentByUuid(uuid);
            if (enr == null)
            {
                return NotFound();
            }
            LoadEnrollment();
            return View(enr);
        }
        [HttpPost]
        public IActionResult Edit(EnrollmentUpdateDto enrollmentUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _enrollmentsService.UpdateEnrollment(enrollmentUpdateDto);
                    return RedirectToAction("Index");
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
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
            var enr = _enrollmentsService.GetEnrollmentByUuid(uuid);
            if (enr == null)
            {
                return NotFound();
            }
            return View(enr);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var enr = _enrollmentsService.GetEnrollmentByUuid(uuid);
            if (enr == null)
            {
                return NotFound();
            }

            _enrollmentsService.DeleteEnrollment(uuid);
            return RedirectToAction("Index");
        }

        public void LoadEnrollment()
        {
            var students = _enrollmentsService.GetStudents();
            SelectList selectListItems1 = new SelectList(students, "Id", "LastName");
            ViewBag.Students = selectListItems1;
            var courseSections = _enrollmentsService.GetCourseSections();
            SelectList selectListItems2 = new SelectList(courseSections, "Id", "SectionNumber");
            ViewBag.CourseSections = selectListItems2;
        }
    }
}
