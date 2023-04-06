using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.User;
using MovieRS.API.Error;
using MovieRS.API.Models;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static IDictionary<string, TokenDto> TokenAccountMap = new Dictionary<string, TokenDto>();

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetUser()
        {
            User? user = HttpContext.Items["User"] as User;
            return user == null 
                ? BadRequest(new ApiException("Something wrong", System.Net.HttpStatusCode.BadRequest))
                : Ok(new ApiResponse<UserDto>(_mapper.Map<UserDto>(user), "OK"));
        }

        [HttpGet]
        [Route("favourite")]
        public IActionResult GetFavourites()
        {
            User? user = HttpContext.Items["User"] as User;

            return user == null
                ? BadRequest(new ApiException("Something wrong", System.Net.HttpStatusCode.BadRequest))
                : Ok(new ApiResponse<UserDto>(_mapper.Map<UserDto>(user), "OK"));
        }
    }
}
