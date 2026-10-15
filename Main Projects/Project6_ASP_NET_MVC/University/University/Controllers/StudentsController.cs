using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Dtos;
using University.Services.Base;

namespace University.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        //private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
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
            var studentsDtos = _studentService.GetAllStudents();

            return View(studentsDtos);
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

                _studentService.AddStudent(studentCreateDto);
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
            var std = _studentService.GetStudentByUuid(uuid);

            if (std == null)
            {
                return NotFound();
            }

            LoadStudents();
            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(StudentUpdateDto studentUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _studentService.UpdateStudent(studentUpdateDto);
                    return RedirectToAction("Index");
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
            }

            ModelState.AddModelError("", "Please fill all the required fields");
            LoadStudents();
            return View(studentUpdateDto);


        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var std = _studentService.GetStudentByUuid(uuid);
            if (std == null)
            {
                return NotFound();
            }

            return View(std);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var std = _studentService.GetStudentByUuid(uuid);
            if (std == null)
            {
                return NotFound();
            }

            _studentService.DeleteStudent(uuid);
            return RedirectToAction("Index");
        }
        public void LoadStudents()
        {
            var degreePrograms = _studentService.GetDegreePrograms();
            SelectList selectListItems = new SelectList(degreePrograms, "Id", "Name");
            ViewBag.DegreePrograms = selectListItems;
        }
    }
}
