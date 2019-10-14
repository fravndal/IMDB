using Microsoft.EntityFrameworkCore;
using Domain;

namespace Persistence
{
    public class IMDBDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }


        public IMDBDbContext(DbContextOptions<IMDBDbContext> options) : base(options)
        { Database.Migrate(); }

        public IMDBDbContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            DbConnection db = new DbConnection();
            string connString = db.GetDbString();
            options.UseSqlServer(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("seq", schema: "IMDB").StartsAt(0).IncrementsBy(1);
            modelBuilder.Entity<Movie>().HasKey(sc => new{ sc.Id });
            modelBuilder.Entity<Genre>().HasKey(sc => new { sc.Id });
            modelBuilder.Entity<MovieGenre>().HasKey(s => new { s.MovieId, s.GenreId });
        }
    }
}
