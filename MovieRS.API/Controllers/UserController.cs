using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.Favourite;
using MovieRS.API.Dtos.History;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos.Recommendation;
using MovieRS.API.Dtos.Search;
using MovieRS.API.Dtos.User;
using MovieRS.API.Error;
using MovieRS.API.Models;
using TMDbLib.Objects.General;

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

        [HttpGet]
        [Route("favourite")]
        [Produces(typeof(ApiResponse<SearchContainerWithIdDto<MovieDto>>))]
        public async Task<IActionResult> GetFavourites([FromQuery] int? page, int? take)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            SearchContainerWithId<TMDbLib.Objects.Movies.Movie> movies = await _unitOfWork.Favorite.GetFavourites(user, page ?? 1, take ?? 0);
            return Ok(new ApiResponse<SearchContainerWithIdDto<MovieDto>>(_mapper.Map<SearchContainerWithIdDto<MovieDto>>(movies), "OK"));
        }

        [HttpPost]
        [Route("favourite/{idMovie}")]
        public async Task<IActionResult> AddFavourite(int idMovie)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            var result = await _unitOfWork.Favorite.NewFavourites(new NewFavouriteDto { UserId = user.Id, MovieId = idMovie });
            return result
                ? Ok(new ApiResponse<bool>(true, "OK"))
                : NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound));
        }

        [HttpDelete]
        [Route("favourite/{idMovie}")]
        public async Task<IActionResult> RemoveFavourite(int idMovie)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            bool result = await _unitOfWork.Favorite.DeleteFavourites(user, idMovie);
            return result
                ? Ok(new ApiResponse<bool>(true, "OK"))
                : NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound));
        }

        [HttpGet]
        [Route("histories")]
        [Produces(typeof(ApiResponse<SearchContainerWithIdDto<HistoryDto>>))]
        public async Task<IActionResult> GetHistories([FromQuery] int? page, int? take)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            SearchContainerWithId<HistoryMovie> movies = await _unitOfWork.HistoryMovie.GetHistories(user, page ?? 1, take ?? 0);
            return Ok(new ApiResponse<SearchContainerWithIdDto<HistoryDto>>(_mapper.Map<SearchContainerWithIdDto<HistoryDto>>(movies), "OK"));
        }

        [HttpPost]
        [Route("histories/{idMovie}")]
        public async Task<IActionResult> AddHistory(int idMovie)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            var result = await _unitOfWork.HistoryMovie.AddHistory(user, idMovie);
            return result
                ? Ok(new ApiResponse<bool>(true, "OK"))
                : NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound));
        }

        [HttpDelete]
        [Route("histories")]
        public async Task<IActionResult> ClearHistory()
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            await _unitOfWork.HistoryMovie.ClearHistory(user);
            return Ok(new ApiResponse<bool>(true, "OK"));
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

        [HttpPost]
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
    }
}
