using System.Collections.Generic;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Api.Helpers;
using Microsoft.AspNetCore.Routing;
using Api.Services;

namespace Api.Controllers
{
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private IIMDBRepository _iIMDBRepository;
        protected readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private ILogger<MoviesController> _logger;

        public MoviesController(IIMDBRepository iMDBRepository, LinkGenerator linkGenerator, IMapper mapper, ILogger<MoviesController> logger)
        {
            _iIMDBRepository = iMDBRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _logger = logger;
        }



        [HttpGet(Name = "GetMovies")]
        public IActionResult GetMovies(MoviesResourceParameters moviesResourceParameters)
        {
            var moviesFromRepo = _iIMDBRepository.GetMovies(moviesResourceParameters);

            var previousPageLink = moviesFromRepo.HasPrevious ?
                CreateMoviesResourceUri(moviesResourceParameters, 
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = moviesFromRepo.HasNext ?
                CreateMoviesResourceUri(moviesResourceParameters, 
                ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = moviesFromRepo.TotalCount,
                pageSize = moviesFromRepo.PageSize,
                currentPage = moviesFromRepo.CurrentPage,
                totalPages = moviesFromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            var movies = _mapper.Map<IEnumerable<MovieDto>>(moviesFromRepo);

            return Ok(movies);
        }

        private string CreateMoviesResourceUri(MoviesResourceParameters moviesResourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _linkGenerator.GetPathByAction(HttpContext, "GetMovies", 
                      values: new
                      {
                          searchQuery = moviesResourceParameters.SearchQuery,
                          genre = moviesResourceParameters.Genre,
                          pageNumber = moviesResourceParameters.PageNumber - 1,
                          pageSize = moviesResourceParameters.PageSize
                      });
                case ResourceUriType.NextPage:
                    return _linkGenerator.GetPathByAction(HttpContext, "GetMovies",
                      values: new
                      {
                          searchQuery = moviesResourceParameters.SearchQuery,
                          genre = moviesResourceParameters.Genre,
                          pageNumber = moviesResourceParameters.PageNumber + 1,
                          pageSize = moviesResourceParameters.PageSize
                      });

                default:
                    return _linkGenerator.GetPathByAction(HttpContext, "GetMovies",
                      values: new
                      {
                          searchQuery = moviesResourceParameters.SearchQuery,
                          genre = moviesResourceParameters.Genre,
                          pageNumber = moviesResourceParameters.PageNumber,
                          pageSize = moviesResourceParameters.PageSize
                    });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            var movieFromRepo = _iIMDBRepository.GetMovie(id);
            if (movieFromRepo == null)
            {
                return NotFound();
            }

            var movie = _mapper.Map<MovieDto>(movieFromRepo);


            return Ok(movie);
        }
    }
}