namespace M_TV.Repository.MoviesRepository
{
    public interface IMoviesRepo
    {
        public Task Create(CreateMovieViewModel newMovie);
        public List<Movie> GetAll();
        public Movie? GetByID(int id);
    }
}
