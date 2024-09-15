namespace M_TV.Repository.MoviesRepository
{
    public interface IMoviesRepo
    {
        public Task Create(CreateMovieViewModel newMovie);
    }
}
