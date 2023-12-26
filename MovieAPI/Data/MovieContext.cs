using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Cast> Casts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Movies)
                .WithMany(m => m.Genres)
                .UsingEntity<MovieGenre>();

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Movies)
                .WithMany(m => m.Persons)
                .UsingEntity<Cast>();
        }
    }
}
