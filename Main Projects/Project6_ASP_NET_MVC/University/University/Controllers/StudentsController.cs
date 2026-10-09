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
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        //public IActionResult Index()
        //{
        //    IEnumerable<Student> students = _db.Students.ToList();
        //    return View(students);
        //}

        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<Student> students = await _db.Students
        //    //more will be added    
        //        .Include(w=> w.WaitlistEntries)
        //        .Include(e => e.Enrollments)
        //        .Include(dp => dp.DegreeProgram).ToListAsync();

        //    return View(students);
        //}

        public IActionResult Index()
        {
            var students = _studentRepository.GetAll();
            IEnumerable<StudentDto> studentDtos = students.Select(s => new StudentDto
            {
                Id = s.Id,
                Uuid = s.Uuid,
                Name = s.DegreeProgram != null ? s.DegreeProgram.Name : "",
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Phone = s.Phone,
                Standing = s.Standing,
                GPA = s.GPA,
                EnrollmentStatus = s.EnrollmentStatus
            }).ToList();
            return View(studentDtos);
        }


        //==============================
        // Create
        //==============================
        [HttpGet]
        public IActionResult Create()
        {

            LoadStudents();
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateDto studentCreateDto)
        {
            if (ModelState.IsValid)
            {
                var newStudent = new Student
                {
                    Uuid = Guid.NewGuid().ToString(),
                    DegreeProgramId = studentCreateDto.DegreeProgramId,
                    FirstName = studentCreateDto.FirstName,
                    LastName = studentCreateDto.LastName,
                    Email = studentCreateDto.Email,
                    Phone = studentCreateDto.Phone,
                    Standing = studentCreateDto.Standing,
                    GPA = studentCreateDto.GPA,
                    EnrollmentStatus = studentCreateDto.EnrollmentStatus
                };
                _studentRepository.AddStudent(newStudent);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill in all required fields.");
            LoadStudents();
            return View(studentCreateDto);
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var std = _studentRepository.GetStudentByUuid(uuid);

            if (std == null)
            {
                return NotFound();
            }
            var dto = new StudentUpdateDto
            {
                Uuid = std.Uuid,
                DegreeProgramId = std.DegreeProgramId,
                FirstName = std.FirstName,
                LastName = std.LastName,
                Email = std.Email,
                Phone = std.Phone,
                Standing = std.Standing,
                GPA = std.GPA,
                EnrollmentStatus = std.EnrollmentStatus
            };
            LoadStudents();
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit(StudentUpdateDto studentUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var oldstd = _studentRepository.GetStudentByUuid(studentUpdateDto.Uuid);
                if(oldstd == null)
                {
                    return NotFound();
                }

                // replaces Update
                oldstd.DegreeProgramId = studentUpdateDto.DegreeProgramId;
                oldstd.FirstName = studentUpdateDto.FirstName;
                oldstd.LastName = studentUpdateDto.LastName;
                oldstd.Email= studentUpdateDto.Email;
                oldstd.Phone = studentUpdateDto.Phone;
                oldstd.Standing = studentUpdateDto.Standing;
                oldstd.GPA= studentUpdateDto.GPA;
                oldstd.EnrollmentStatus = studentUpdateDto.EnrollmentStatus;

                _studentRepository.UpdateStudent(oldstd);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields");
            return View(studentUpdateDto);


        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var std = _studentRepository.GetStudentByUuid(uuid);
            if (std == null)
            {
                return NotFound();
            }
            var dto = new StudentUpdateDto
            {
                Uuid = std.Uuid,
                DegreeProgramId = std.DegreeProgramId,
                FirstName = std.FirstName,
                LastName = std.LastName,
                Email = std.Email,
                Phone = std.Phone,
                Standing = std.Standing,
                GPA = std.GPA,
                EnrollmentStatus = std.EnrollmentStatus
            };
            return View(dto);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var std = _studentRepository.GetStudentByUuid(uuid);
            if (std == null)
            {
                return NotFound();
            }

            _studentRepository.DeleteStudent(uuid);
            return RedirectToAction("Index");
        }
        public void LoadStudents()
        {
            var degreePrograms = _studentRepository.GetDegreePrograms();
            SelectList selectListItems = new SelectList(degreePrograms, "Id", "Name");
            ViewBag.DegreePrograms = selectListItems;
        }
    }
}
