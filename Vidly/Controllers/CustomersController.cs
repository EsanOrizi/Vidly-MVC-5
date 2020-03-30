using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
// import this otherwise will not recongnise viewModels, NewCustomerViewModel
using Vidly.ViewModels;

// to have Include functionality
using System.Data.Entity;

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



        // add a new action result for new customer page
        public ActionResult New()
        {
            // get the list of membership types from the database
            // MemebershipTypes is the DbSet
            var membershipTypes = _context.MembershipTypes.ToList();
            // we need to create a view model that encapsulates all the data required by this view
            // so create newCustopmerViewModel
            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes
            };

            // then pass it to the view
            return View(viewModel);
        }


        // HttpPost Attribure to this action to make sure it can only be called by HttpPost not HttpGet
        [HttpPost]
        // Create method
        public ActionResult Create(Customer customer)
        {
            return View();
        }


        public ViewResult Index()
        {
            // 4. get the customer list from the database 
            // 5. when this line is executed, entity framework is not going to query the database, deferred execution
            // the query is excuted when we iterate over the object
            // .ToList after the customer executes the query staright away


            // to eager load membershipType
            // var customers = _context.Customers.ToList();
            // TO THIS var customers = _context.Customers.Include(c => c.MembershipType).ToList();


            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }


        


        public ActionResult Details(int id)
        {
            // 6. get the data from the database, becuase of SingleOrDefault our query is executed straight away
            // var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // Solution to section 3 part 2 exercise, Include(c => c.MembershipType) is to add MembershipType to customer, so can be used in customer details view
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

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