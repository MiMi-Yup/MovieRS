using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Error;

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
        [Produces(typeof(ApiResponse<SearchContainerDto<MovieDto>>))]
        public async Task<IActionResult> Search(string query, [FromQuery] int? page, [FromQuery] string? region, [FromQuery] int? year, [FromQuery] int? primaryReleaseYear)
        {
            var movie = await _unitOfWork.Movie.SearchMovie(query, year ?? 0, region, primaryReleaseYear ?? 0, page ?? 1);
            return movie == null 
                ? BadRequest(new ApiException("Something wrong", System.Net.HttpStatusCode.BadRequest)) 
                : Ok(new ApiResponse<SearchContainerDto<MovieDto>>(_mapper.Map<SearchContainerDto<MovieDto>>(movie), "OK"));
        }

        [HttpGet]
        [Route("person/{query}")]
        [Produces(typeof(ApiResponse<SearchContainerDto<PersonDto>>))]
        public async Task<IActionResult> Search(string query, [FromQuery] int? page, [FromQuery] string? region)
        {
            var person = await _unitOfWork.Person.SearchPerson(query, page ?? 1, region);
            return person == null 
                ? BadRequest(new ApiException("Something wrong", System.Net.HttpStatusCode.BadRequest))
                : Ok(new ApiResponse<SearchContainerDto<PersonDto>>(_mapper.Map<SearchContainerDto<PersonDto>>(person), "OK"));
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
