using Microsoft.AspNetCore.Mvc;
using MovieRS.Video.Core.Contracts;
using MovieRS.Video.Dtos;

namespace MovieRS.Video.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private static readonly IDictionary<string, string> mineType = new Dictionary<string, string> {
            { ".mp4", "video/mp4" },
            { ".ts", "video/mp2t" },
            { ".avi", "video/x-msvideo" },
            { ".mkv", "video/x-matroska" }
        };

        public VideoController(ILogger<VideoController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            FileInfo? file = await _unitOfWork.Video.GetVideo(id);
            if (file != null && file.Exists)
            {
                Stream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, true);
                string contentType = mineType.ContainsKey(file.Extension.ToLower()) ? mineType[file.Extension.ToLower()] : "application/octet-stream";
                return File(stream, contentType, true);
            }
            return NotFound();
        }
    }
}
