using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;

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
        public async Task<IActionResult> GetImage(string path)
        {
            byte[] image = await _unitOfWork.Image.GetImage($"/{path}");
            return image == null ? NotFound() : File(image, "image/jpeg", true);
        }
    }
}
