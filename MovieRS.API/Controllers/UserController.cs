using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
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

        [HttpPut]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto change)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user != null && await _unitOfWork.User.UpdatePassword(user, change))
                return Ok();
            return BadRequest(new
            {
                message = "Wrong old password"
            });
        }

        [HttpGet]
        [Route("recommendation")]
        [Produces(typeof(ApiResponse<ResultRecommendationDto>))]
        public async Task<IActionResult> Recommendation([FromQuery] int? takeMax)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not supported this feature", System.Net.HttpStatusCode.Unauthorized));
            var movie = await _unitOfWork.Recommend.GetUserMovieRecommendation(user!, takeMax ?? 0);
            return Ok(new ApiResponse<ResultRecommendationDto>(_mapper.Map<ResultRecommendationDto>(movie), "OK"));
        }
    }
}
