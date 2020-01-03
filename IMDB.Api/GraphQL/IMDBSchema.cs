using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Api.GraphQL
{
    public class IMDBSchema : Schema
    {
        public IMDBSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<IMDBQuery>();
        }
    }
}
