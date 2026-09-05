using Bootcamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp_MVC_EF.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult GetCustomers()
        {
            IList<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe", Email = "John@Doe.com", Phone = "123-456-7890" },
                new Customer { Id = 2, Name = "Jane Smith", Email = "Jane@Smith.com", Phone = "987-654-3210" },
                new Customer { Id = 3, Name = "Bob Johnson", Email = "Bob Johnson.com", Phone = "555-555-5555" },
                };
            return Ok(customers);
        }

        public IActionResult Index()
        {
            IList<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe", Email = "John@Doe.com", Phone = "123-456-7890" },
                new Customer { Id = 2, Name = "Jane Smith", Email = "Jane@Smith.com", Phone = "987-654-3210" },
                new Customer { Id = 3, Name = "Bob Johnson", Email = "Bob Johnson.com", Phone = "555-555-5555" },
                };
            return View(customers);
        }
    }
}
