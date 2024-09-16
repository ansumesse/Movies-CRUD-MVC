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
            var movies = moviesRepo.GetAll();
            return View(movies);
        }
        public IActionResult Details(int id)
        {
            Movie? movie = moviesRepo.GetByID(id);
            if (movie is null)
                return NotFound();
            return View(movie);
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
		
		public IActionResult AllowedExtensionsAction(IFormFile Cover)
		{
            if (Cover is not null)
            {
                string extension = Path.GetExtension(Cover.FileName);

                bool isAllowed = FileSettings.AllowedImageExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    return Json($"Only {FileSettings.AllowedImageExtensions} are allowed.");
                }

                if (Cover.Length > FileSettings.MaxFileSizeInBytes)
                {
                    return Json($"Cover size must be less than {FileSettings.MaxFileSizeInBytes} bytes.");
                }

            }
            return Json(true);
        }
		public IActionResult MaxFileSizeAction(IFormFile file)
		{
            if (file is not null)
            {
                if (file.Length > FileSettings.MaxFileSizeInBytes)
                {
                    return Json(false);
                }
            }
            return Json(true);
        }
		
	}
}
