using Bootcamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp_MVC_EF.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            IList<Category> categories = new List<Category>
            {
                new Category {Id = 1, Name = "Electronics", Description ="laptops, headphones, and televisions"},
                new Category {Id = 2, Name = "Books", Description ="fiction, non-fiction, and educational books"},
                new Category {Id = 3, Name = "Clothing", Description ="men's, women's, and children's apparel"},
                new Category {Id = 4, Name = "Toys", Description ="educational, electronic, and outdoor toys"}
            };

            return View(categories);
        }
    }
}
