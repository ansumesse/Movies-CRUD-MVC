
using M_TV.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace M_TV.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

			modelBuilder.Entity<Category>()
				.HasData(new List<Category>
				{
					new Category(){Id = 1, Name ="Action"},
					new Category(){Id = 2, Name ="Adventure"},
					new Category(){Id = 3, Name ="Animation"},
					new Category(){Id = 4, Name ="Anime"},
					new Category(){Id = 5, Name ="Comedy"},
					new Category(){Id = 6, Name ="Crime"},
					new Category(){Id = 7, Name ="Documentary"},
					new Category(){Id = 8, Name ="Drama"},
					new Category(){Id = 9, Name ="Family"},
					new Category(){Id = 10, Name ="Horror"}
				});
			modelBuilder.Entity<Actor>()
				.HasData(new List<Actor>()
				{
					new Actor(){Id = 1, Name  = "Morgan Freeman"},
					new Actor(){Id = 2, Name  = "Marlon Brando"},
					new Actor(){Id = 3, Name  = "Al Pacino"}
				});

		}

	}
}
