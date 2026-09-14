using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;

namespace University.Controllers
{
    public class WaitlistEntrysController : Controller
    {
        private readonly AppDbContext _db;

        public WaitlistEntrysController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<WaitlistEntry> waitlistEntries = _db.WaitlistEntries.ToList();
            return View(waitlistEntries);
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
        public IActionResult Create(WaitlistEntry waitlistEntry)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(waitlistEntry);
            }
            _db.WaitlistEntries.Add(waitlistEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //==============================
        // Edit
        //==============================
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var wti = _db.WaitlistEntries.Find(Id);
            if (wti == null)
            {
                return NotFound();
            }
            return View(wti);
        }
        [HttpPost]
        public IActionResult Edit(WaitlistEntry waitlistEntry)
        {
            if (ModelState.IsValid)
            {
                _db.WaitlistEntries.Update(waitlistEntry);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(waitlistEntry);
        }

        //===========
        // Delete
        //===========
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var wti = _db.WaitlistEntries.Find(Id);
            if (wti == null)
            {
                return NotFound();
            }
            return View(wti);
        }
        [HttpPost]
        public IActionResult Delete(WaitlistEntry waitlistEntry)
        {
            _db.WaitlistEntries.Remove(waitlistEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
