using Domain;
using Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IIMDBRepository
    {
        PagedList<Movie> GetMovies(MoviesResourceParameters moviesResourceParameters);
        
        Movie GetMovie(int id);
        IEnumerable<Genre> GetGenres();
        Genre GetGenre(int id);
    }
}
