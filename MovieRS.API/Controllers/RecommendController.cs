using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.Recommendation;
using MovieRS.API.Error;
using MovieRS.API.Models;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RecommendController : ControllerBase
    {
        private readonly ILogger<RecommendController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecommendController(ILogger<RecommendController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        [Route("recommendation-cf")]
        [Produces(typeof(ApiResponse<ResultRecommendationDto>))]
        public async Task<IActionResult> RecommendationCF([FromQuery] int? takeMax)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not supported this feature", System.Net.HttpStatusCode.Unauthorized));
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
                return Unauthorized(new ApiException("User not supported this feature", System.Net.HttpStatusCode.Unauthorized));
            var movie = await _unitOfWork.Recommend.GetMovieGenreRecommendation(user!, takeMax ?? 0);
            return Ok(new ApiResponse<ResultRecommendationDto>(_mapper.Map<ResultRecommendationDto>(movie), "OK"));
        }
    }
}
