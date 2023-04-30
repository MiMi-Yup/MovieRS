using MovieRS.API.Dtos;
using MovieRS.API.Dtos.User;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Contracts
{
    public interface IUserRepository
    {
        Task<(User?, String)> Login(LoginDto loginDto);
        Task<User?> GetById(string id);
        Task<User?> FindByEmail(string email);
        Task<bool> UpdatePassword(LoginDto updateAccount);
        Task UpdateCountry(User user, CountryDto countryDto);
        Task<User> CreateNewUser(RegisterUserDto registerUserDto);
    }
}
