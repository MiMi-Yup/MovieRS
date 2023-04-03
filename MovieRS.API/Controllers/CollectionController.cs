using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Error;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CollectionController : ControllerBase
    {
        private readonly ILogger<CollectionController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CollectionController(ILogger<CollectionController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        [Produces(typeof(ApiResponse<CollectionMovieDto>))]
        public async Task<IActionResult> Collection(int id)
        {
            var collection = await _unitOfWork.CollectionMovie.GetCollection(id);
            return collection == null 
                ? NotFound(new ApiException("Id not exist", null)) 
                : Ok(new ApiResponse<CollectionMovieDto>(_mapper.Map<CollectionMovieDto>(collection), message: "OK"));
        }
    }
}
