using GraphQL.Types;
using IMDB.Api.GraphQL.Types;
using IMDB.Api.Helpers;
using IMDB.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Api.GraphQL
{
    public class IMDBQuery : ObjectGraphType
    {
        public IMDBQuery(IIMDBRepository repository)
        {
            Field<ListGraphType<MovieType>>(
                "movies",
                resolve: context => repository.GetMovies()
            );
        }
    }
}
