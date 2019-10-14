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
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            // Map for Movies
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                src.ConvertGenre()));

            //CreateMap<Movie, MovieDto>();

            // Map for Book
            /*CreateMap<Book, BookDto>();

            CreateMap<Models.AuthorForCreationDto, Entities.Author>();

            CreateMap<Models.BookForCreatingDto, Entities.Book>();*/
        }
    }
}
