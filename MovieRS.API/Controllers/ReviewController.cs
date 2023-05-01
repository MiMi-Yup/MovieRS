using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.History;
using MovieRS.API.Dtos.Review;
using MovieRS.API.Dtos.Search;
using MovieRS.API.Error;
using MovieRS.API.Models;
using TMDbLib.Objects.General;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewController(ILogger<ReviewController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [Produces(typeof(ApiResponse<SearchContainerWithIdDto<UserReviewDto>>))]
        public async Task<IActionResult> GetReviews([FromQuery] int? page, int? take)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            var reviews = await _unitOfWork.Review.GetReviews(user, page ?? 1, take ?? 0);
            return Ok(new ApiResponse<SearchContainerWithIdDto<UserReviewDto>>(_mapper.Map<SearchContainerWithIdDto<UserReviewDto>>(reviews), "OK"));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DetailReview(string id)
        {
            if (int.TryParse(id, out int localIdReview))
            {
                TMDbLib.Objects.Reviews.ReviewBaseExtension? review = await _unitOfWork.Review.GetReviewById(localIdReview);
                if (review != null)
                {
                    return Ok(new ApiResponse<ReviewDto>(_mapper.Map<ReviewDto>(review), "OK"));
                }
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> NewReview(NewReviewDto newReview)
        {
            try
            {
                User? user = HttpContext.Items["User"] as User;
                if (user != null)
                {
                    await _unitOfWork.Movie.NewReview(user, newReview);
                    return Ok(new ApiResponse<bool>(true, "OK"));
                }
                else
                    return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));

            }
            catch (Exception)
            {
                return BadRequest(new ApiException("Error new review", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto updateReview)
        {
            try
            {
                User? user = HttpContext.Items["User"] as User;
                if (user != null)
                {
                    bool success = await _unitOfWork.Review.UpdateReview(user, updateReview);
                    return success ? Ok() : BadRequest();
                }
                else
                    return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));

            }
            catch (Exception)
            {
                return BadRequest(new ApiException("Error update review", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteReview(string id)
        {
            try
            {
                User? user = HttpContext.Items["User"] as User;
                if (user != null)
                {
                    if (int.TryParse(id, out int localIdReview))
                    {
                        await _unitOfWork.Review.DeleteReview(user, localIdReview);
                        return Ok();
                    }
                    return NoContent();
                }
                else
                    return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));

            }
            catch (Exception)
            {
                return BadRequest(new ApiException("Error delete review", System.Net.HttpStatusCode.InternalServerError));
            }
        }
    }
}
