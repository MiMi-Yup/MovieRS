using Microsoft.IdentityModel.Tokens;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MovieRS.API.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }


        private string? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var accountId = ValidateToken(token);
                if (accountId != null)
                {
                    // attach user to context on successful jwt validation
                    User? user = await unitOfWork.User.GetById(accountId);
                    context.Items["User"] = user;
                    context.Items["UserId"] = user.Id;
                }
            }
            await _next(context);
        }
    }
}
