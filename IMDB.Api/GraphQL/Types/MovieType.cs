using Domain;
using GraphQL.Types;
using IMDB.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Api.GraphQL.Types
{
    public class MovieType : ObjectGraphType<Movie>
    {
        public MovieType()
        {
            Field(t => t.Id);
            Field(t => t.Type);
            Field(t => t.PrimaryTitle);
            Field(t => t.OriginalTitle);
            Field(t => t.IsAdult);
            Field(t => t.StartYear);
            Field(t => t.EndYear);
            Field(t => t.RuntimeMinutes);


        }
    }
}
