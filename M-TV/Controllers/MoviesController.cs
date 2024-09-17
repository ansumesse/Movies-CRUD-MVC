namespace M_TV.Controllers
{
    [Authorize]
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
		public async Task<IActionResult> Create(CreateMovieViewModel newMovieVM)
		{
			if (ModelState.IsValid)
			{
				await moviesRepo.Create(newMovieVM);
				return RedirectToAction(nameof(Index));
			}

			newMovieVM.Actors = actorsRepo.GetSelectListsActors();
			newMovieVM.Categories = categoriesRepo.GetSelectListsCatigories();
			return View(newMovieVM);
		}
        [HttpGet]
        public IActionResult Update(int id)
        {
            var movie = moviesRepo.GetByID(id);
            if (movie is null)
                return NotFound();
            UpdateMovieViewModel viewModel = new()
            {
                Id = movie.Id,
                Name = movie.Name,
                CategoryId = movie.CategoryId,
                Discription = movie.Discription,
                Rate = movie.Rate,
                CoverName = movie.Cover,
                Actors = actorsRepo.GetSelectListsActors(),
                Categories = categoriesRepo.GetSelectListsCatigories()
            };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateMovieViewModel editedMovieVM)
        {
            if (ModelState.IsValid)
            {
                var movie = await moviesRepo.Update(editedMovieVM);
                if (movie is null)
                    return BadRequest();
                return RedirectToAction(nameof(Index));
            }
            editedMovieVM.Actors = actorsRepo.GetSelectListsActors();
            editedMovieVM.Categories = categoriesRepo.GetSelectListsCatigories();
            return View(editedMovieVM);

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = moviesRepo.Delete(id);
            return isDeleted ? Ok() : BadRequest();
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
