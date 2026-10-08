using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly AppDbContext _db;

        public StudentsController(AppDbContext db)
        {
            _db = db;
        }
        //public IActionResult Index()
        //{
        //    IEnumerable<Student> students = _db.Students.ToList();
        //    return View(students);
        //}

        public async Task<IActionResult> Index()
        {
            IEnumerable<Student> students = await _db.Students
            //more will be added    
                .Include(w=> w.WaitlistEntries)
                .Include(e => e.Enrollments).ToListAsync();

            return View(students);
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
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(student);
            }
            
            _db.Students.Add(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(string uuid)
        {
            var std = _db.Students.FirstOrDefault(s => s.Uuid == uuid);

            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                var oldstd = _db.Students.FirstOrDefault(s => s.Uuid == student.Uuid);
                if(oldstd == null)
                {
                    return NotFound();
                }

                // replaces Update
                oldstd.DegreeProgramId = student.DegreeProgramId;
                oldstd.FirstName = student.FirstName;
                oldstd.LastName = student.LastName;
                oldstd.Email= student.Email;
                oldstd.Phone = student.Phone;
                oldstd.Standing = student.Standing;
                oldstd.GPA= student.GPA;
                oldstd.EnrollmentStatus = student.EnrollmentStatus;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields");
            return View(student);


        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(string uuid)
        {
            var std = _db.Students.FirstOrDefault(s => s.Uuid == uuid);
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
            var std = _db.Students.FirstOrDefault(s => s.Uuid == uuid);
            if (std == null)
            {
                return NotFound();
            }

            _db.Students.Remove(std);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
