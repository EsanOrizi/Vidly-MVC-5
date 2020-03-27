using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        
        // 1. need a db context to access the database
        private ApplicationDbContext _context;

        // 2. initialize the _context
        public CustomersController()
        {

            _context = new ApplicationDbContext();

        }

        // 3. db context is a disposable object so we need to properly dispose of it
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ViewResult Index()
        {
            // 4. get the customer list from the database 
            // 5. when this line is executed, entity framework is not going to query the database, deferred execution
            // the query is excuted when we iterate over the object
            // .ToList after the customer executes the query staright away
            var customers = _context.Customers.ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            // 6. get the data from the database, becuase of SingleOrDefault our query is executed straight away
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }




        // 0. get rid of this method and get the list of customers from the database
        /*
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "John Smith" },
                new Customer { Id = 2, Name = "Mary Williams" }
            };
        }
        */
    }
}