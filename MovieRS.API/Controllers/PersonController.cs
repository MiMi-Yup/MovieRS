using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos;
using MovieRS.API.Error;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonController(ILogger<PersonController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        [Produces(typeof(ApiResponse<PersonDto>))]
        public async Task<IActionResult> Person(int id)
        {
            var movie = await _unitOfWork.Person.GetPerson(id);
            return movie == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<PersonDto>(_mapper.Map<PersonDto>(movie), "OK"));
        }

        [HttpGet]
        [Route("{id}/movies")]
        [Produces(typeof(ApiResponse<MovieCreditsDto>))]
        public async Task<IActionResult> Movies(int id)
        {
            var movie = await _unitOfWork.Person.GetMovieAct(_unitOfWork.Movie, id);
            return movie == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<MovieCreditsDto>(_mapper.Map<MovieCreditsDto>(movie), "OK"));
        }
    }
}
