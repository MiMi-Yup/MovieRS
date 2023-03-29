using MovieRS.API.Dtos.User;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Contracts
{
    public interface IUserRepository
    {
        Task<(User?, String)> Login(LoginDto loginDto);
        Task<User?> GetById(string id);
        Task<bool> FindByEmail(string email);
        Task<User> CreateNewUser(RegisterUserDto registerUserDto);
    }
}
