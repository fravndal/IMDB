using Domain;
using IMDB.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Api.Services
{
    public interface IIMDBRepository
    {
        PagedList<Movie> GetMovies(MoviesResourceParameters moviesResourceParameters);
        
        Movie GetMovie(int id);
        IEnumerable<Genre> GetGenres();
        Genre GetGenre(int id);
    }
}
