using Bootcamp_MVC_EF.Data;
using Bootcamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp_MVC_EF.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products.ToList();
            return View(products);
        }
    }
}
