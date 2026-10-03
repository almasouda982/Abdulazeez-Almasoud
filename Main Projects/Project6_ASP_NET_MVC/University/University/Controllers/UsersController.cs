using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
    //[Authorize]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }


        // =========================
        // Index
        // =========================
        public IActionResult Index()
        {
            var users = _db.Users.ToList();

            return View(users);
        }


        // =========================
        // Details
        // =========================
        public IActionResult Details(int id)
        {
            var user = _db.Users.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }


        // =========================
        // Create GET
        // =========================
        public IActionResult Create()
        {
            return View();
        }


        // =========================
        // Create POST
        // =========================
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _db.Users.Add(user);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }


        // =========================
        // Edit GET
        // =========================
        public IActionResult Edit(string uuid)
        {
            var user = _db.Users.FirstOrDefault(u => u.Uuid == uuid);


            if (user == null)
                return NotFound();

            return View(user);
        }


        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var oldUser = _db.Users.FirstOrDefault(u => u.Uuid == u.Uuid);

                if (oldUser == null)
                    return NotFound();

                oldUser.Name = user.Name;
                oldUser.UserName = user.UserName;
                oldUser.Email = user.Email;
                oldUser.IsLocked = user.IsLocked;


                if (!string.IsNullOrEmpty(user.Password))
                {

                    oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all required fields");
            return View(user);
        }


        // =========================
        // Delete GET
        // =========================
        public IActionResult Delete(string uuid)
        {
            var user = _db.Users.FirstOrDefault(u => u.Uuid == uuid);

            if (user == null)
                return NotFound();

            return View(user);
        }



        // POST
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string uuid)
        {
            var user = _db.Users.FirstOrDefault(u => u.Uuid == uuid);

            if (user == null)
                return NotFound();

            _db.Users.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
