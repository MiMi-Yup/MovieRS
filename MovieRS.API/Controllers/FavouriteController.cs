using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.Favourite;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos.Search;
using MovieRS.API.Error;
using MovieRS.API.Models;
using TMDbLib.Objects.General;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavouriteController : ControllerBase
    {
        private readonly ILogger<FavouriteController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FavouriteController(ILogger<FavouriteController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [Produces(typeof(ApiResponse<SearchContainerWithIdDto<FavouriteDto>>))]
        public async Task<IActionResult> GetFavourites([FromQuery] int? page, [FromQuery] int? take)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            SearchContainerWithId<UserFavourite> movies = await _unitOfWork.Favorite.GetFavourites(user, page ?? 1, take ?? 0);
            return Ok(new ApiResponse<SearchContainerWithIdDto<FavouriteDto>>(_mapper.Map<SearchContainerWithIdDto<FavouriteDto>>(movies), "OK"));
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddFavourite([FromBody] int idMovie)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            var result = await _unitOfWork.Favorite.NewFavourites(new NewFavouriteDto { UserId = user.Id, MovieId = idMovie });
            switch (result)
            {
                case 0:
                    return Ok(new ApiResponse<bool>(true, "OK"));
                case 1:
                    return BadRequest(new { message = "Has already favourited" });
                case -1:
                    return NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound));
                default:
                    return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveFavourite(int id)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            bool result = await _unitOfWork.Favorite.DeleteFavourites(user, id);
            return result
                ? Ok(new ApiResponse<bool>(true, "OK"))
                : NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound));
        }
    }
}
