
using System.Diagnostics;

namespace M_TV.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMoviesRepo moviesRepo;

        public HomeController(IMoviesRepo moviesRepo)
        {
            this.moviesRepo = moviesRepo;
        }

        public IActionResult Index()
        {
            var movies = moviesRepo.GetAll();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
