using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos.Recommendation;
using MovieRS.API.Dtos.User;
using MovieRS.API.Error;
using MovieRS.API.Models;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static IDictionary<string, TokenDto> TokenAccountMap = new Dictionary<string, TokenDto>();

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetUser()
        {
            User? user = HttpContext.Items["User"] as User;
            return user == null
                ? BadRequest(new ApiException("Something wrong", System.Net.HttpStatusCode.BadRequest))
                : Ok(new ApiResponse<UserDto>(_mapper.Map<UserDto>(user), "OK"));
        }

        [HttpGet]
        [Route("favourite")]
        public IActionResult GetFavourites()
        {
            User? user = HttpContext.Items["User"] as User;

            return user == null
                ? BadRequest(new ApiException("Something wrong", System.Net.HttpStatusCode.BadRequest))
                : Ok(new ApiResponse<UserDto>(_mapper.Map<UserDto>(user), "OK"));
        }

        [HttpGet]
        [Route("recommendation-cf")]
        [Produces(typeof(ApiResponse<ResultRecommendationDto>))]
        public async Task<IActionResult> RecommendationCF([FromQuery] int? takeMax)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return NotFound(new ApiException("User not supported this feature", System.Net.HttpStatusCode.NotFound));
            var movie = await _unitOfWork.Recommend.GetUserMovieRecommendation(user!, takeMax ?? 0);
            return Ok(new ApiResponse<ResultRecommendationDto>(_mapper.Map<ResultRecommendationDto>(movie), "OK"));
        }

        [HttpGet]
        [Route("recommendation-cb")]
        [Produces(typeof(ApiResponse<ResultRecommendationDto>))]
        public async Task<IActionResult> RecommendationCB([FromQuery] int? takeMax)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return NotFound(new ApiException("User not supported this feature", System.Net.HttpStatusCode.NotFound));
            var movie = await _unitOfWork.Recommend.GetMovieGenreRecommendation(user!, takeMax ?? 0);
            return Ok(new ApiResponse<ResultRecommendationDto>(_mapper.Map<ResultRecommendationDto>(movie), "OK"));
        }
    }
}
