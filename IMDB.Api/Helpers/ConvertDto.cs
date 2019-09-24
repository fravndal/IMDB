using Domain;
using IMDB.Api.Models;
using IMDB.Api.Services;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Api.Helpers
{
    public static class ConvertDto
    {
        public static List<GenreDto> ConvertGenre(this Movie movie)
        {
            var genresDto = new List<GenreDto>();
            foreach (var movieGenre in movie.MovieGenres)
            {
                genresDto.Add(new GenreDto()
                {
                    Id = movieGenre.Genre.Id,
                    Name = movieGenre.Genre.Name
                });
            }
            return genresDto;
        }
    }
}
