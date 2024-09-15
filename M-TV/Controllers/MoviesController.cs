namespace M_TV.Controllers
{
	public class MoviesController : Controller
	{
        private readonly ApplicationDbContext context;
        private readonly ICategoriesRepo categoriesRepo;
        private readonly IActorsRepo actorsRepo;
        private readonly IMoviesRepo moviesRepo;

        public MoviesController(ApplicationDbContext context,
			ICategoriesRepo categoriesRepo, 
			IActorsRepo actorsRepo,
			IMoviesRepo moviesRepo)
        {
            this.context = context;
            this.categoriesRepo = categoriesRepo;
            this.actorsRepo = actorsRepo;
            this.moviesRepo = moviesRepo;
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
				Categories = categoriesRepo.GetSelectListsCatigories(),
				Actors = actorsRepo.GetSelectListsActors()
			};
			return View(MoviesVM);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateMovieViewModel newMovie)
		{
			if (ModelState.IsValid)
			{
				await moviesRepo.Create(newMovie);
				return RedirectToAction(nameof(Index));
			}

			newMovie.Actors = actorsRepo.GetSelectListsActors();
			newMovie.Categories = categoriesRepo.GetSelectListsCatigories();
			return View(newMovie);
		}
		
	}
}
