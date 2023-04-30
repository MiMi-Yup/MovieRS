using Microsoft.IdentityModel.Tokens;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Helper;
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

            try
            {
                IDictionary<string, string> map = _configuration.ParseToken(token);
                if (map.TryGetValue("UserId", out string? userId))
                    return userId;
                return null;
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
