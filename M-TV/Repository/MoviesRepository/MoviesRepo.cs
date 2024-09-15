namespace M_TV.Repository.MoviesRepository
{
    public class MoviesRepo : IMoviesRepo
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string imagesPath;

        public MoviesRepo(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            imagesPath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        public async Task Create(CreateMovieViewModel newMovie)
        {
            string coverName = $"{Guid.NewGuid()}{Path.GetExtension(newMovie.Cover.FileName)}";
            string path = Path.Combine(imagesPath, coverName);

            using var stream = File.Create(path);
            await newMovie.Cover.CopyToAsync(stream);

            Movie movie = new Movie()
            {
                Cover = coverName,
                CategoryId = newMovie.CategoryId,
                Actors = newMovie.SelectedActors.Select(a => new MovieActor() { ActorId = a }).ToList(),
                Discription = newMovie.Discription,
                Name = newMovie.Name,
                Rate = newMovie.Rate
            };
            context.Add(movie);
            context.SaveChanges();
        }
    }
}
