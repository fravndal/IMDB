using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Api.Helpers;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Api.Services
{
    public class IMDBRepository : IIMDBRepository
    {
        private IMDBDbContext _context;

        public IMDBRepository(IMDBDbContext context)
        {
            _context = context;
        }

        public PagedList<Movie> GetMovies(MoviesResourceParameters moviesResourceParameters)
        {
            var collectionBeforePaging = _context.Movies
                .Include(x => x.MovieGenres)
                .ThenInclude(y => y.Genre)
                //.Take(moviesResourceParameters.PageSize)
                .AsQueryable();

            if (!string.IsNullOrEmpty(moviesResourceParameters.Genre))
            {
                // trim & ignore casing
                /*var genreForWhereClause = moviesResourceParameters.Genre
                    .Trim().ToLowerInvariant();*/
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.MovieGenres.Any(x => x.Genre.Name == moviesResourceParameters.Genre));
            }

            if (!string.IsNullOrEmpty(moviesResourceParameters.SearchQuery))
            {
                // trim & ignore casing
                /*var searchQueryForWhereClause = moviesResourceParameters.SearchQuery
                    .Trim().ToLowerInvariant();*/

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.PrimaryTitle.Contains(moviesResourceParameters.SearchQuery)
                    || a.OriginalTitle.Contains(moviesResourceParameters.SearchQuery)
                    || a.MovieGenres.Any(x => x.Genre.Name.Contains( moviesResourceParameters.SearchQuery)));
            }

            return PagedList<Movie>.Create(collectionBeforePaging,
                moviesResourceParameters.PageNumber,
                moviesResourceParameters.PageSize);
                
                
                
                //check out lazy loading
        }

        

        public Movie GetMovie(int id)
        {
            return _context.Movies.Include(x => x.MovieGenres).ThenInclude(y => y.Genre).FirstOrDefault(z => z.Id == id);
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.OrderBy(n => n.Name);
        }
        public Genre GetGenre(int genreId)
        {
            return _context.Genres.FirstOrDefault(m => m.Id == genreId);
        }
    }
}
