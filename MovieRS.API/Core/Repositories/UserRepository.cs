﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.User;
using MovieRS.API.Error;
using MovieRS.API.Helper;
using MovieRS.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieRS.API.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ICountryRepository _countryRepository;

        public UserRepository(MovieRsContext context, IConfiguration configuration, ICountryRepository countryRepository, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
            _configuration = configuration;
            _countryRepository = countryRepository;
        }

        public async Task<User> CreateNewUser(RegisterUserDto registerUserDto)
        {
            Country? country = string.IsNullOrWhiteSpace(registerUserDto.CountryCode) 
                ? null 
                : await _countryRepository.GetByCode(registerUserDto.CountryCode);
            User newUser = new User
            {
                Username = registerUserDto.Username,
                Password = HashHelper.Hash(registerUserDto.Password),
                Email = registerUserDto.Email,
                CountryId = country?.Id
            };
            await this.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User?> FindByEmail(string email)
        {
            try
            {
                return await this.Find(user => user.Email == email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repository} FindUserByEmail function error", typeof(UserRepository));
                return null;
            }
        }

        public async Task<User?> GetById(string id)
        {
            return await dbSet.Include(u => u.Country).SingleOrDefaultAsync(item => item.Id.ToString() == id);
        }

        public async Task<(User?, String)> Login(LoginDto loginDto)
        {
            byte[]? hashPassword = HashHelper.Hash(loginDto.Password);

            User? user = await dbSet.Include(u => u.Country).SingleOrDefaultAsync(item => item.Email == loginDto.Email && item.Password == hashPassword);

            if (user != null)
            {
                string token = GenerateToken(user);
                return (user, token);
            }

            return (null, string.Empty);
        }

        private string GenerateToken(User account)
        {
            return _configuration.GenerateToken(new Dictionary<string, string> {
                { "UserId", account.Id.ToString() },
                { ClaimTypes.NameIdentifier, account.Id.ToString() }
            });
        }

        public async Task<bool> UpdatePassword(LoginDto updateAccount)
        {
            User? user = await dbSet.FirstOrDefaultAsync(item => item.Email == updateAccount.Email);
            if (user != null)
            {
                user.Password = HashHelper.Hash(updateAccount.Password);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task UpdateCountry(User user, CountryDto countryDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePassword(User user, ChangePasswordDto changePassword)
        {
            if (string.IsNullOrWhiteSpace(changePassword.OldPassword)
                || string.IsNullOrWhiteSpace(changePassword.NewPassword))
                return false;
            byte[] bytesOld = HashHelper.Hash(changePassword.OldPassword);
            
            if (Enumerable.SequenceEqual(user.Password, bytesOld))
            {
                User? find = await dbSet.FindAsync(user.Id);
                if (find != null)
                {
                    find.Password = HashHelper.Hash(changePassword.NewPassword);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
