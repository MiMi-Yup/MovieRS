using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos.User;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/movie")]
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
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet]
        [Route("{id}/images")]
        public async Task<IActionResult> Images(int id)
        {
            var movie = await _unitOfWork.Movie.GetImages(id);
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet]
        [Route("{id}/recommendation")]
        public async Task<IActionResult> Recommendation(int id)
        {
            var movie = await _unitOfWork.Movie.GetRecommendation(id);
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet]
        [Route("{id}/review")]
        public async Task<IActionResult> Review(int id)
        {
            var movie = await _unitOfWork.Movie.GetReview(id);
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet]
        [Route("{id}/collection")]
        public async Task<IActionResult> Collection(int id)
        {
            var movie = await _unitOfWork.Movie.GetCollection(id);
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet]
        [Route("popular")]
        public async Task<IActionResult> Popular()
        {
            var movie = await _unitOfWork.Movie.GetPopular();
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet]
        [Route("now_playing")]
        public async Task<IActionResult> NowPlaying()
        {
            var movie = await _unitOfWork.Movie.GetNowPlaying();
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet]
        [Route("upcoming")]
        public async Task<IActionResult> UpComing()
        {
            var movie = await _unitOfWork.Movie.GetUpComming();
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpGet]
        [Route("top_rated")]
        public async Task<IActionResult> TopRated()
        {
            var movie = await _unitOfWork.Movie.GetTopRated();
            return Ok(_mapper.Map<MovieDto>(movie));
        }
    }
}
