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
            // so create newCustomerViewModel
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            // then pass it to the view
            return View("CustomerForm", viewModel);
        }


        // HttpPost Attribute to this action to make sure it can only be called by HttpPost not HttpGet
        [HttpPost]
        // Create method
        public ActionResult Save(Customer customer)
        {
            
            // if customer is not in the database , i.e. a new customer 
            if (customer.Id == 0)
            
            // add the customer to our db context, customer here is just in the memory
            _context.Customers.Add(customer);
            // so changes are persistent, to save the changes, its goes through changes and add sql statements to save the data to database
            // else add the customer to db
            else 
            {
                // customer object in database
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                // update the values
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

            }
            
            
            // save changes into database
            _context.SaveChanges();
            // redirect the user to list of customers
            return RedirectToAction("Index", "Customers");
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

        // Edit action 
        public ActionResult Edit(int id)
        {
            // need to get the customer with this Id from the database
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            // if the customer exists in the database it will be returned otherwise we get null
            // so need to check for that
            if (customer == null)
                return HttpNotFound();
            // otherwise use the given customer to render customer form
            var viewModel = new CustomerFormViewModel
            {
                // set cusomer to this object
                Customer = customer,
                // initialize membership types, get it from database
                MembershipTypes = _context.MembershipTypes.ToList()
             }; 
        
            
            // we need to override convension and give return the view to return otherwise it will return edit view
            // second argument we pass the viewModel to our view
            return View("CustomerForm", viewModel);
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