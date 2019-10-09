using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.IO;
using System.Linq;

namespace Testing
{
    class Program
    {
        private static IMDBDbContext _context = new IMDBDbContext();
        static void Main(string[] args)
        {
            /*var context = IMDBContextFactory.GetDbContext();

            var movie = context.Movies.Include(x => x.MovieGenres).ThenInclude(y => y.Genre).FirstOrDefault(z => z.Id == 3);


            Console.WriteLine(movie);*/

            //GetGenres();
            //GetGenre();
            //GetGenresWildcard();
            var test = @"\Data\";

            Console.WriteLine(Environment.CurrentDirectory+test);
            
        }

        private static void GetGenresWildcard()
        {
            var genresWildcard = _context.Genres.Where(g => EF.Functions.Like(g.Name, "S%")).ToList();
            foreach(var genre in genresWildcard)
            {
                Console.WriteLine(genre.Name);
            }
        }

        private static void GetGenres()
        {
            
            var genres = _context.Genres.ToList();
            foreach(var genre in genres)
            {
                Console.WriteLine(genre.Name);
            }

            
        }
        private static void GetGenre()
        {
            using (var context = new IMDBDbContext())
            {
                var genre = context.Genres.Find(2);
                Console.WriteLine(genre.Name);

            }
        }
    }
}
