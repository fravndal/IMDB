using AutoMapper;
using Domain;
using IMDB.Api.Helpers;
using IMDB.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Api.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            // Map for Genre
            CreateMap<Genre, GenreDto>();

            
        }
    }
}