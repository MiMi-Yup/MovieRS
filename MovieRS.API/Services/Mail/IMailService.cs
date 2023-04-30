using MovieRS.API.Dtos.User;

namespace MovieRS.API.Services.Mail
{
    public interface IMailService
    {
        public Task SendRegisterMail(UserDto user, string token, bool isCreated = false);
        public Task SendForgotPasswordEmail(UserDto user, string token);
    }
}
