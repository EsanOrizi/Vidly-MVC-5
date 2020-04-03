using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
// need this
using System.Linq;
// need this for (m => m.Genre)
using System.Data.Entity;
using System.Web.UI.WebControls;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {


        // 1. need a db context to access the database
        private ApplicationDbContext _context;

        // 2. initialize the _context
        public MoviesController()
        {

            _context = new ApplicationDbContext();

        }

        // 3. db context is a disposable object so we need to properly dispose of it
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new NewMovieViewModel {Genres = genres};

            return View(viewModel);
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


            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }





        //public ViewResult Index()
        //{
        //    var movies = GetMovies();

        //    return View(movies);    
        //}

        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-e" }
        //    };
        //}



        public ActionResult Details(int id)
        {
            // 6. get the data from the database, becuase of SingleOrDefault our query is executed straight away
            // var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // Solution to section 3 part 2 exercise, Include(c => c.MembershipType) is to add MembershipType to customer, so can be used in customer details view
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }












        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }
    }
}