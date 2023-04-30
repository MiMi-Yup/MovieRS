using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.History;
using MovieRS.API.Dtos.Search;
using MovieRS.API.Error;
using MovieRS.API.Models;
using TMDbLib.Objects.General;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HistoryController : ControllerBase
    {
        private readonly ILogger<HistoryController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HistoryController(ILogger<HistoryController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
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
        [Route("")]
        public async Task<IActionResult> AddHistory([FromBody] int id)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            var result = await _unitOfWork.HistoryMovie.AddHistory(user, id);
            return result
                ? Ok(new ApiResponse<bool>(true, "OK"))
                : NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            var result = await _unitOfWork.HistoryMovie.DeleteHistory(user, id);
            return result
                ? Ok(new ApiResponse<bool>(true, "OK"))
                : NotFound(new ApiException("Id not found", System.Net.HttpStatusCode.NotFound));
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> ClearHistory()
        {
            User? user = HttpContext.Items["User"] as User;
            if (user == null)
                return Unauthorized(new ApiException("User not exists", System.Net.HttpStatusCode.Unauthorized));
            await _unitOfWork.HistoryMovie.ClearHistory(user);
            return Ok(new ApiResponse<bool>(true, "OK"));
        }
    }
}
