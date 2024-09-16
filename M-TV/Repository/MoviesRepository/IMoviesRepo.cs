namespace M_TV.Repository.MoviesRepository
{
    public interface IMoviesRepo
    {
        public Task Create(CreateMovieViewModel newMovie);
        public List<Movie> GetAll();
        public Movie? GetByID(int id);
        public Task<Movie?> Update(UpdateMovieViewModel model);
        public bool Delete(int id);
    }
}
