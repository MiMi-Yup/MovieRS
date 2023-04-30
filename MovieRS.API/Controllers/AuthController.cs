using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRS.API.Attributes;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.User;
using MovieRS.API.Error;
using MovieRS.API.Models;
using MovieRS.API.Services.Mail;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace MovieRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private static IDictionary<string, TokenDto> TokenAccountMap = new Dictionary<string, TokenDto>();

        public AuthController(ILogger<AuthController> logger, IUnitOfWork unitOfWork, IMapper mapper, IMailService mailService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailService = mailService;
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
                    token = user.token,
                    message = "Login successful"
                });
            }
            catch (ApiException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto newUser)
        {
            bool item = await _unitOfWork.User.FindByEmail(newUser.Email);
            if (item)
            {
                return BadRequest(new ApiException("Email have already existed!!!", System.Net.HttpStatusCode.BadRequest));
            }

            var code = new TokenDto { Value = GenerateCode(), ExpiredAt = DateTime.Now.AddMinutes(15), User = newUser };
            if (TokenAccountMap.ContainsKey(newUser.Email))
            {
                TokenAccountMap.Remove(newUser.Email);
            }
            TokenAccountMap.Add(newUser.Email, code);

            _ = _mailService.SendRegisterMail(new UserDto { Email = code.User.Email }, code.Value);

            return Ok(new
            {
                message = "Send email verification successfully"
            });
        }

        [HttpPost]
        [Route("verify-account")]
        public async Task<IActionResult> VerifyEmailToken(TokenVerificationDto data)
        {
            if (!TokenAccountMap.TryGetValue(data.Email, out TokenDto? token) || data.Code != token.Value || token.ExpiredAt < DateTime.Now)
            {
                return BadRequest(new
                {
                    message = "Code is wrong or expired!"
                });
            }

            await _unitOfWork.User.CreateNewUser(token.User);

            TokenAccountMap.Remove(data.Email);

            (User? user, string token) user = await _unitOfWork.User.Login(new LoginDto { Email = token.User.Email, Password = token.User.Password });
            UserDto returnUser = _mapper.Map<UserDto>(user.user);

            return Ok(new
            {
                data = returnUser,
                token = user.token,
                message = "Login successful"
            });
        }

        private string GenerateCode()
        {
            return new Random().Next(1000, 9999).ToString("D4");
        }
    }
}
