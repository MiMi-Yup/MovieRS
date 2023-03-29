using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Attributes;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.User;
using MovieRS.API.Error;
using MovieRS.API.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static IDictionary<string, TokenDto> TokenAccountMap = new Dictionary<string, TokenDto>();

        public AuthController(ILogger<AuthController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginUser)
        {
            (User? user, string token) user = await _unitOfWork.User.Login(loginUser);
            if (user.user != null)
            {

            }
            else
            {
                return NotFound(new ApiException("Wrong email or password", null));
            }
            UserDto userDto = _mapper.Map<UserDto>(user.user);
            try
            {
                
                return Ok(new
                {
                    data = userDto,
                    token = user.token
                });
            }
            catch (ApiException exception)
            {
                return NotFound(exception);
            }
        }

        /*[HttpGet]
        [Route("verify")]
        [Protect]
        public IActionResult Verify()
        {
            User? user = HttpContext.Items["User"] as User;
            UserDto dto = _mapper.Map<UserDto>(user);
            return Ok(new ApiResponse<UserDto>(dto, "Verify successfully"));
        }*/

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto newUser)
        {
            bool item = await _unitOfWork.User.FindByEmail(newUser.Email);
            if (item)
            {
                return BadRequest(new ApiException("Email have already existed!!!", System.Net.HttpStatusCode.BadRequest));
            };

            var code = new TokenDto { Value = GenerateCode(), ExpiredAt = DateTime.Now.AddMinutes(15), User = newUser };
            if (TokenAccountMap.ContainsKey(newUser.Email))
            {
                TokenAccountMap.Remove(newUser.Email);
            }
            TokenAccountMap.Add(newUser.Email, code);
            /*await _mailService.SendRegisterMail(new UserDto { Email = user.Email }, code.Value);*/
            Console.WriteLine($"New code {code.User.Email} - {code.Value}");
            return Ok(new
            {
                message = "Send email verification successfully"
            });
        }

        [HttpPost]
        [Route("verify-account")]
        public async Task<IActionResult> VerifyEmailToken(TokenVerificationDto data)
        {
            TokenDto token;
            if (!TokenAccountMap.TryGetValue(data.Email, out token!) || data.Code != token.Value || token.ExpiredAt < DateTime.Now)
            {
                return BadRequest(new
                {
                    message = "Code is wrong or expired!"
                });
            }
            var registerData = token.User;

            User newUser = await _unitOfWork.User.CreateNewUser(registerData);
            TokenAccountMap.Remove(data.Email);
            UserDto returnUser = _mapper.Map<UserDto>(newUser);
            return Ok(new
            {
                message = "Verify account successfully!",
                data = returnUser
            });
        }

        private string GenerateCode()
        {
            return new Random().Next(1000, 9999).ToString("D4");
        }
    }
}
