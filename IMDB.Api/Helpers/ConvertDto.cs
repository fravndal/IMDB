using Domain;
using Api.Models;
using System.Collections.Generic;

namespace Api.Helpers
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
