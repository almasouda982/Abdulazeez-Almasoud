using Bootcamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp_MVC_EF.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            IList<Product> products = new List<Product>
            {
                new Product { Id = 1, ProductName="Hair Dryer", Category= "Electronics", Price = 20 },
                new Product { Id = 2, ProductName="Harry Potter First Collection", Category="Books", Price=150 },
                new Product { Id = 3, ProductName="Arsenal Home Kit", Category="Clothing", Price=200 },
                new Product { Id = 4, ProductName="Lego Star Wars Set", Category="Toys", Price=100 }
            };

            return View(products);
        }
    }
}
