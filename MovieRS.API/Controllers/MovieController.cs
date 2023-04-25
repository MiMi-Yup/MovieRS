using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Attributes;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos.Review;
using MovieRS.API.Dtos.Search;
using MovieRS.API.Error;
using MovieRS.API.Models;
using System.Text.Json.Serialization;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieController(ILogger<MovieController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("video-api")]
        [Protect]
        public async Task<IActionResult> UpdateVideoApi(VideoDomainDto domain)
        {
            bool success = string.IsNullOrEmpty(domain.Domain) 
                ? false 
                : await _unitOfWork.VideoAPI.UpdateDomain(domain.Domain);
            return success ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        [Produces(typeof(ApiResponse<MovieDto>))]
        public async Task<IActionResult> Movie(int id)
        {
            var movie = await _unitOfWork.Movie.GetMovieBy3rd(id);
            return movie == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<MovieDto>(_mapper.Map<MovieDto>(movie), "OK"));
        }

        [HttpGet]
        [Route("{id}/videos")]
        [Produces(typeof(ApiResponse<ResultContainerDto<VideoDto>>))]
        public async Task<IActionResult> Videos(int id)
        {
            var video = await _unitOfWork.Movie.GetVideos(id);
            return video == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<ResultContainerDto<VideoDto>>(_mapper.Map<ResultContainerDto<VideoDto>>(video), "OK"));
        }

        [HttpGet]
        [Route("{id}/actors")]
        [Produces(typeof(ApiResponse<CreditsDto>))]
        public async Task<IActionResult> Actors(int id, [FromQuery] int? take)
        {
            var movie = await _unitOfWork.Movie.GetActors(_unitOfWork.Person, id, take ?? 0);
            return movie == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<CreditsDto>(_mapper.Map<CreditsDto>(movie), "OK"));
        }

        [HttpGet]
        [Route("{id}/images")]
        [Produces(typeof(ApiResponse<ImageDto>))]
        public async Task<IActionResult> Images(int id, [FromQuery] int? take)
        {
            var image = await _unitOfWork.Movie.GetImages(id, take ?? 0);
            return image == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<ImageDto>(_mapper.Map<ImageDto>(image), "OK"));
        }

        [HttpGet]
        [Route("{id}/recommendation")]
        [Produces(typeof(ApiResponse<SearchContainerDto<MovieDto>>))]
        public async Task<IActionResult> Recommendation(int id, [FromQuery] int? page, [FromQuery] int? take)
        {
            var movie = await _unitOfWork.Movie.GetRecommendation(id, page ?? 1, take ?? 0);
            return movie == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<SearchContainerDto<MovieDto>>(_mapper.Map<SearchContainerDto<MovieDto>>(movie), "OK"));
        }

        [HttpGet]
        [Route("{id}/review")]
        [Produces(typeof(ApiResponse<SearchContainerWithIdDto<ReviewDto>>))]
        public async Task<IActionResult> Review(int id, [FromQuery] int? page, [FromQuery] int? take)
        {
            var movie = await _unitOfWork.Movie.GetReview(id, page ?? 1, take ?? 0);
            return movie == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<SearchContainerWithIdDto<ReviewDto>>(_mapper.Map<SearchContainerWithIdDto<ReviewDto>>(movie), "OK"));
        }

        [HttpPost]
        [Route("{id}/review")]
        public async Task<IActionResult> NewReview(int id, NewReviewDto newReview)
        {
            try
            {
                User? user = HttpContext.Items["User"] as User;
                if (user != null)
                {
                    await _unitOfWork.Movie.NewReview(user, id, newReview);
                    return Ok(new ApiResponse<bool>(true, "OK"));
                }
                else
                    return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiException("Error new review", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("popular")]
        [Produces(typeof(ApiResponse<SearchContainerDto<MovieDto>>))]
        public async Task<IActionResult> Popular([FromQuery] int? page, [FromQuery] int? take)
        {
            var movie = await _unitOfWork.Movie.GetPopular(page ?? 1, take ?? 0);
            return movie == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<SearchContainerDto<MovieDto>>(_mapper.Map<SearchContainerDto<MovieDto>>(movie), "OK"));
        }

        [HttpGet]
        [Route("now_playing")]
        [Produces(typeof(ApiResponse<SearchContainerWithDataRangeDto<MovieDto>>))]
        public async Task<IActionResult> NowPlaying([FromQuery] int? page, [FromQuery] int? take)
        {
            var movie = await _unitOfWork.Movie.GetNowPlaying(page ?? 1, take ?? 0);
            return movie == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<SearchContainerWithDataRangeDto<MovieDto>>(_mapper.Map<SearchContainerWithDataRangeDto<MovieDto>>(movie), "OK"));
        }

        [HttpGet]
        [Route("upcoming")]
        [Produces(typeof(ApiResponse<SearchContainerWithDataRangeDto<MovieDto>>))]
        public async Task<IActionResult> UpComing([FromQuery] int? page, [FromQuery] int? take)
        {
            var movie = await _unitOfWork.Movie.GetUpComming(page ?? 1, take ?? 0);
            return movie == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<SearchContainerWithDataRangeDto<MovieDto>>(_mapper.Map<SearchContainerWithDataRangeDto<MovieDto>>(movie), "OK"));
        }

        [HttpGet]
        [Route("top_rated")]
        [Produces(typeof(ApiResponse<SearchContainerDto<MovieDto>>))]
        public async Task<IActionResult> TopRated([FromQuery] int? page, [FromQuery] int? take)
        {
            var movie = await _unitOfWork.Movie.GetTopRated(page ?? 1, take ?? 0);
            return movie == null
                ? NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound))
                : Ok(new ApiResponse<SearchContainerDto<MovieDto>>(_mapper.Map<SearchContainerDto<MovieDto>>(movie), "OK"));
        }
    }
}
