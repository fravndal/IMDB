using System.Collections.Generic;


namespace Domain

{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }

        public Genre()
        {
            MovieGenres = new List<MovieGenre>();
        }

    }
}
