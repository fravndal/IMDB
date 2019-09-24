using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Models;
using IMDB.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Api.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private IIMDBRepository _iIMDBRepository;
        private readonly IMapper _mapper;

        public GenresController(IIMDBRepository iMDBRepository, IMapper mapper)
        {
            _iIMDBRepository = iMDBRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult GetGenres()
        {
            var genresFromRepo = _iIMDBRepository.GetGenres();

            var genres = _mapper.Map<IEnumerable<GenreDto>>(genresFromRepo);

            return new JsonResult(genres);
        }

        [HttpGet("{id}")]
        public IActionResult GetGenre(int id)
        {
            var genreFromRepo = _iIMDBRepository.GetGenre(id);

            var genre = _mapper.Map<GenreDto>(genreFromRepo);

            return new JsonResult(genre);
        }






    }
}