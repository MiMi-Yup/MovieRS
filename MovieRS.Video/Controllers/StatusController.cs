using Microsoft.AspNetCore.Mvc;
using MovieRS.Video.Core.Contracts;
using MovieRS.Video.Dtos;

namespace MovieRS.Video.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<StatusController> _logger;

        public StatusController(IUnitOfWork unitOfWork, ILogger<StatusController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Produces(typeof(ApiResponse<bool>))]
        public IActionResult Index()
        {
            return Ok(new ApiResponse<bool>(true, "Alive"));
        }

        [HttpGet]
        [Route("{id}")]
        [Produces(typeof(ApiResponse<StatusVideoDto>))]
        public async Task<IActionResult> GetStatus(long id)
        {
            StatusVideoDto? status = await _unitOfWork.Video.GetStatus(id);
            if (status == null || !status.Available) return NotFound(new ApiResponse<StatusVideoDto>(null!, "Not exist video in file server"));
            return Ok(new ApiResponse<StatusVideoDto>(status, "Ok"));
        }
    }
}
