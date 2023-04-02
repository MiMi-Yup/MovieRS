using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos.User;

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

        [HttpGet]
        [Route("{id}")]
        [Produces(typeof(MovieDto))]
        public async Task<IActionResult> Movie(int id)
        {
            var movie = await _unitOfWork.Movie.GetMovie(id);
            return movie == null ? NotFound() : Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet]
        [Route("{id}/images")]
        [Produces(typeof(ImageDto))]
        public async Task<IActionResult> Images(int id)
        {
            var movie = await _unitOfWork.Movie.GetImages(id);
            return movie == null ? NotFound() : Ok(_mapper.Map<ImageDto>(movie));
        }

        [HttpGet]
        [Route("{id}/recommendation")]
        [Produces(typeof(SearchContainerDto<MovieDto>))]
        public async Task<IActionResult> Recommendation(int id)
        {
            var movie = await _unitOfWork.Movie.GetRecommendation(id);
            return movie == null ? NotFound() : Ok(_mapper.Map<SearchContainerDto<MovieDto>>(movie));
        }

        [HttpGet]
        [Route("{id}/review")]
        [Produces(typeof(SeachContainerWithIdDto<ReviewDto>))]
        public async Task<IActionResult> Review(int id)
        {
            var movie = await _unitOfWork.Movie.GetReview(id);
            return movie == null ? NotFound() : Ok(_mapper.Map<SeachContainerWithIdDto<ReviewDto>>(movie));
        }

        [HttpGet]
        [Route("popular")]
        [Produces(typeof(SearchContainerDto<MovieDto>))]
        public async Task<IActionResult> Popular()
        {
            var movie = await _unitOfWork.Movie.GetPopular();
            return movie == null ? NotFound() : Ok(_mapper.Map<SearchContainerDto<MovieDto>>(movie));
        }

        [HttpGet]
        [Route("now_playing")]
        [Produces(typeof(SearchContainerWithDataRangeDto<MovieDto>))]
        public async Task<IActionResult> NowPlaying()
        {
            var movie = await _unitOfWork.Movie.GetNowPlaying();
            return movie == null ? NotFound() : Ok(_mapper.Map<SearchContainerWithDataRangeDto<MovieDto>>(movie));
        }

        [HttpGet]
        [Route("upcoming")]
        [Produces(typeof(SearchContainerWithDataRangeDto<MovieDto>))]
        public async Task<IActionResult> UpComing()
        {
            var movie = await _unitOfWork.Movie.GetUpComming();
            return movie == null ? NotFound() : Ok(_mapper.Map<SearchContainerWithDataRangeDto<MovieDto>>(movie));
        }

        [HttpGet]
        [Route("top_rated")]
        [Produces(typeof(SearchContainerDto<MovieDto>))]
        public async Task<IActionResult> TopRated()
        {
            var movie = await _unitOfWork.Movie.GetTopRated();
            return movie == null ? NotFound() : Ok(_mapper.Map<SearchContainerDto<MovieDto>>(movie));
        }
    }
}
