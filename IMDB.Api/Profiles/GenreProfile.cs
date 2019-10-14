using AutoMapper;
using Domain;
using Api.Helpers;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Profiles
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