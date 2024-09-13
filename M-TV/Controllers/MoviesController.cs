using M_TV.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace M_TV.Controllers
{
	public class MoviesController : Controller
	{
        private readonly ApplicationDbContext context;

        public MoviesController(ApplicationDbContext Db)
        {
            context = Db;
        }
        public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Create()
		{
			CreateMovieViewModel MoviesVM = new()
			{
				Categories = context.Categories
				.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
				.OrderBy(c => c.Text),
				Actors = context.Actors
				.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
				.OrderBy(a => a.Text)
			};
			return View(MoviesVM);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CreateMovieViewModel newMovie)
		{
			return View();
		}
		
	}
}
