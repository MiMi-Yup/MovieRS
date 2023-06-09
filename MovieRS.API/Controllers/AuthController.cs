﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.User;
using MovieRS.API.Error;
using MovieRS.API.Helper;
using MovieRS.API.Models;
using MovieRS.API.Services.Mail;
using System.Security.Cryptography;
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
        private readonly IConfiguration _configuration;
        private static readonly IDictionary<string, TokenDto<RegisterUserDto>> TokenAccountMap = new Dictionary<string, TokenDto<RegisterUserDto>>();
        private static readonly IDictionary<string, TokenDto<ResetAccountDto>> TokenResetAccountMap = new Dictionary<string, TokenDto<ResetAccountDto>>();
        private static readonly SHA256 SHA256 = SHA256.Create();

        public AuthController(
            ILogger<AuthController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration,
            IMailService mailService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailService = mailService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginUser)
        {
            (User? user, string token) user = await _unitOfWork.User.Login(loginUser);
            if (user.user == null)
                return NotFound(new ApiException("Wrong email or password", null));
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
            User? item = await _unitOfWork.User.FindByEmail(newUser.Email);
            if (item != null)
            {
                return BadRequest(new ApiException("Email have already existed!!!", System.Net.HttpStatusCode.BadRequest));
            }

            var code = new TokenDto<RegisterUserDto>
            {
                Value = GenerateCode(),
                ExpiredAt = DateTime.Now.AddMinutes(15),
                User = newUser
            };
            if (TokenAccountMap.ContainsKey(newUser.Email))
            {
                TokenAccountMap.Remove(newUser.Email);
            }
            TokenAccountMap.Add(newUser.Email, code);

            _ = _mailService.SendRegisterMail(new UserDto { Email = code.User.Email }, code.Value);
            Console.Write(code.Value);
            return Ok(new
            {
                message = "Send email verification successfully"
            });
        }

        [HttpPost]
        [Route("verify-account")]
        public async Task<IActionResult> VerifyEmailToken(TokenVerificationDto data)
        {
            if (!TokenAccountMap.TryGetValue(data.Email, out TokenDto<RegisterUserDto>? token) || data.Code != token.Value || token.ExpiredAt < DateTime.Now)
            {
                return BadRequest(new
                {
                    message = "Code is wrong or expired!"
                });
            }

            await _unitOfWork.User.CreateNewUser(token.User!);

            TokenAccountMap.Remove(data.Email);

            (User? user, string token) user = await _unitOfWork.User.Login(new LoginDto
            {
                Email = token.User.Email,
                Password = token.User.Password
            });
            UserDto returnUser = _mapper.Map<UserDto>(user.user);

            return Ok(new
            {
                data = returnUser,
                token = user.token,
                message = "Login successful"
            });
        }

        [HttpPost]
        [Route("lost-account")]
        public async Task<IActionResult> LostAccount(LostAccountDto lostAccount)
        {
            if (string.IsNullOrWhiteSpace(lostAccount.Code) 
                || !TokenResetAccountMap.ContainsKey(lostAccount.Email))
            {
                User? user = await _unitOfWork.User.FindByEmail(lostAccount.Email);
                if (user == null)
                    return NotFound();

                var code = new TokenDto<ResetAccountDto>
                {
                    Value = GenerateCode(),
                    User = new ResetAccountDto
                    {
                        Token = user.Email
                    }
                };

                if (TokenResetAccountMap.ContainsKey(user.Email))
                {
                    TokenResetAccountMap.Remove(user.Email);
                }
                TokenResetAccountMap.Add(user.Email, code);

                _ = _mailService.SendForgotPasswordEmail(new UserDto { Email = code.User.Token }, code.Value);

                return Ok(new
                {
                    message = "Send email verification successfully"
                });

            }
            else if (TokenResetAccountMap.ContainsKey(lostAccount.Email) 
                && TokenResetAccountMap[lostAccount.Email].Value == lostAccount.Code)
            {
                TokenDto<ResetAccountDto> reset = TokenResetAccountMap[lostAccount.Email];
                reset.User!.Token = _configuration.GenerateToken(new Dictionary<string, string> {
                    { "Email", reset.User.Token },
                    { "IP", HttpContext.Request.Host.Value },
                    { "Time", DateTime.Now.ToString() }
                });
                reset.ExpiredAt = DateTime.Now.AddMinutes(15);

                return Ok(new
                {
                    token = reset.User.Token,
                    message = "Code valid"
                });
            }
            return BadRequest(new
            {
                message = "Code invalid"
            });
        }

        [HttpPost]
        [Route("reset-account")]
        public async Task<IActionResult> ResetAccount(ResetAccountDto resetAccount)
        {
            IDictionary<string, string>? parseToken = null;
            try
            {
                parseToken = _configuration.ParseToken(resetAccount.Token);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "JWT Wrong" });
            }

            if (!parseToken.TryGetValue("Email", out string? email) || !TokenResetAccountMap.TryGetValue(email, out TokenDto<ResetAccountDto>? token)
                || resetAccount.Token != token.User?.Token
                || token.ExpiredAt < DateTime.Now)
                return BadRequest(new
                {
                    message = "Code is wrong or expired!"
                });

            LoginDto login = new LoginDto { Email = email, Password = resetAccount.Password };
            bool success = await _unitOfWork.User.UpdatePassword(login);

            if (!success)
                return BadRequest(new { message = "Something wrong" });

            TokenAccountMap.Remove(email);

            (User? user, string token) user = await _unitOfWork.User.Login(login);

            UserDto userDto = _mapper.Map<UserDto>(user.user);
            return Ok(new
            {
                data = userDto,
                token = user.token,
                message = "Reset successful"
            });
        }

        private string GenerateCode()
        {
            return new Random().Next(10000, 99999).ToString("D5");
        }
    }
}
