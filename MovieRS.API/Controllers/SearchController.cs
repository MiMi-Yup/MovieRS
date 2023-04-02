using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.Movie;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchController(ILogger<SearchController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("movie/{query}")]
        [Produces(typeof(SearchContainerDto<MovieDto>))]
        public async Task<IActionResult> Search(string query, [FromQuery] int? page, [FromQuery] string? region, [FromQuery] int? year, [FromQuery] int? primaryReleaseYear)
        {
            var movie = await _unitOfWork.Movie.SearchMovie(query, year ?? 0, region, primaryReleaseYear ?? 0, page ?? 1);
            return movie == null ? NotFound() : Ok(_mapper.Map<SearchContainerDto<MovieDto>>(movie));
        }

        [HttpGet]
        [Route("person/{query}")]
        [Produces(typeof(SearchContainerDto<PersonDto>))]
        public async Task<IActionResult> Search(string query, [FromQuery] int? page, [FromQuery] string? region)
        {
            var person = await _unitOfWork.Person.SearchPerson(query, page ?? 1, region);
            return person == null ? NotFound() : Ok(_mapper.Map<SearchContainerDto<PersonDto>>(person));
        }

        /*[HttpGet]
        [Route("genre/{query}")]
        [Produces(typeof(MovieDto))]
        public Task<IActionResult> Search(string query, [FromQuery] int? page, [FromQuery] int? year)
        {
            return NotFound();
        }*/
    }
}
