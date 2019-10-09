using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Persistence;

namespace Import.Core
{
    public static class ParseObject
    {
        public static Movie ParseMovie(this string line, int id)
        {
            var column = line.Split("\t".ToCharArray());

            return new Movie
            {
                Id = id,
                ExternalId = column[0],
                Type = column[1],
                PrimaryTitle = column[2],
                OriginalTitle = column[3],
                IsAdult = column[4],
                StartYear = (column[5] == "\\N") ? 0 : int.Parse(column[5]),
                EndYear = (column[6] == "\\N") ? 0 : int.Parse(column[6]),
                RuntimeMinutes = (column[7] == "\\N") ? 0 : int.Parse(column[7]),
            };
        }

        public static Genre ParseGenre(this string genre, int id)
        {
            return new Genre
            {
                Id = id,
                Name = genre
            };
        }

        public static List<int> GenreExistInInternalList(this string line, List<Genre> internalGenreList)
        {
            List<int> _id = new List<int>();
            var column = line.Split("\t".ToCharArray());
            var genres = column[8].Split(",");
            foreach (var genre in genres)
            {
                //Add if genre dont exists
                if (!internalGenreList.Any(x => x.Name == genre))
                {
                    var id = internalGenreList.Count() + 1;
                    internalGenreList.Add(genre.ParseGenre(id));
                    _id.Add(id);
                }
                else
                {
                    var id = internalGenreList.FirstOrDefault(x => x.Name == genre).Id;
                    _id.Add(id);
                }
            }
            return _id;
        }

        public static MovieGenre ParseMovieGenre(this int movieId, int genreId)
        {
            return new MovieGenre
            {
                MovieId = movieId,
                GenreId = genreId
            };
        }
    }
}
