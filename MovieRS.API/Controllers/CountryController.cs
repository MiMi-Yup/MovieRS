using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos.Search;
using MovieRS.API.Error;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryController(ILogger<CountryController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [Produces(typeof(ApiResponse<SearchContainerDto<CountryDto>>))]
        public async Task<IActionResult> GetCountries([FromQuery] int? page, [FromQuery] int? take)
        {
            TMDbLib.Objects.General.SearchContainer<Models.Country> container = await _unitOfWork.Country.GetAll(page ?? 0, take ?? 0);
            return Ok(new ApiResponse<SearchContainerDto<CountryDto>>(_mapper.Map<SearchContainerDto<CountryDto>>(container), "OK"));
        }
    }
}