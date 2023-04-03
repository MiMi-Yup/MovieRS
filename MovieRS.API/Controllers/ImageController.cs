using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Error;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageController(ILogger<ImageController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{path}")]
        public async Task<IActionResult> Image(string path)
        {
            try
            {
                byte[] image = await _unitOfWork.Image.GetImage($"/{path}");
                return File(image, "image/jpeg", true);
            }
            catch (ApiException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
