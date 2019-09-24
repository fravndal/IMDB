using System.Collections.Generic;

namespace Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Type { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public string IsAdult { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int RuntimeMinutes { get; set; }
        public List<MovieGenre> MovieGenres { get; set; }

        public Movie()
        {
            MovieGenres = new List<MovieGenre>();
        }

        public Movie(int id, string externalId, string type, string primaryTitle, string originalTitle, string isAdult, int startYear, int endYear, int runtimeMinutes)
        {
            Id = id;
            ExternalId = externalId;
            Type = type;
            PrimaryTitle = primaryTitle;
            OriginalTitle = originalTitle;
            IsAdult = isAdult;
            StartYear = startYear;
            EndYear = endYear;
            RuntimeMinutes = runtimeMinutes;
            
        }
    }


}
