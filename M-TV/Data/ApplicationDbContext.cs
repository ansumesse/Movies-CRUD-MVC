
using M_TV.Models;

namespace M_TV.Data
{
	public class ApplicationDbContext : DbContext
	{
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		protected ApplicationDbContext():base()
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<MovieActor>()
				.HasKey(m => new { m.MovieId, m.ActorId});
		}

	}
}
