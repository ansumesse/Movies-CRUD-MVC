

namespace M_TV.Repository.MoviesRepository
{
    public class MoviesRepo : IMoviesRepo
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string MoviesImagePath;

        public MoviesRepo(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            MoviesImagePath = $"{webHostEnvironment.WebRootPath}{FileSettings.MoviesImagePath}";
        }
        public async Task Create(CreateMovieViewModel newMovie)
        {
            string coverName = await ImageServices.Save(MoviesImagePath, newMovie.Cover);

            Movie movie = new Movie()
            {
                Cover = coverName,
                CategoryId = newMovie.CategoryId,
                MovieActors = newMovie.SelectedActors.Select(a => new MovieActor() { ActorId = a }).ToList(),
                Discription = newMovie.Discription,
                Name = newMovie.Name,
                Rate = newMovie.Rate
            };
            context.Add(movie);
            context.SaveChanges();
        }

        public List<Movie> GetAll()
        {
            List<Movie> movies = context.Movies
                .AsNoTracking()
                .Include(m=> m.Category)
                .ToList();
            return movies;
        }

        public Movie? GetByID(int id)
        {
            return context.Movies
                .Include(m => m.Category)
                .Include(m => m.MovieActors)
                .ThenInclude(m => m.Actor)
                /*.AsNoTracking()*/
                .FirstOrDefault(m => m.Id == id);
        }

        public async Task<Movie?> Update(UpdateMovieViewModel model)
        {
            var movie = GetByID(model.Id);
            if (movie is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = movie.Cover;

            movie.Name = model.Name;
            movie.CategoryId = model.CategoryId;
            movie.Rate = model.Rate;
            movie.Discription = model.Discription;
            movie.MovieActors = model.SelectedActors.Select(a => new MovieActor() { ActorId = a }).ToList();
            
            if(hasNewCover)
                movie.Cover = await ImageServices.Save(MoviesImagePath, model.Cover!);

            var rowEffected = context.SaveChanges();
            if (rowEffected > 0)
            {
                if (hasNewCover)
                {
                    var path = Path.Combine(MoviesImagePath, oldCover);
                    File.Delete(path);
                }
                return movie;
            }
            else
                return null;


        }        
        bool IMoviesRepo.Delete(int id)
        {
            bool isDeleted = false;
            var movie = GetByID(id);
            if (movie is null)
                return isDeleted;
            context.Movies.Remove(movie);
            var rowEffected = context.SaveChanges();
            if (rowEffected > 0)
            {
                File.Delete(Path.Combine(MoviesImagePath, movie.Cover));
                isDeleted = true;
            }
            return isDeleted;

        }

       

    }
}
