using AutoMapper;
using Floristai.Dto;
using Floristai.Entities;
using Floristai.Models;
using Floristai.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Floristai.Services
{
    public class UserService : IUserService
    {
        private readonly string _key;
        private readonly IUserRepository _userRepository;
        private readonly Mapper _mapper;

        public UserService(IUserRepository userRepository, IJwtKeyHoldingService jwtKeyHoldingService, Mapper mapper)
        {
            _userRepository = userRepository;
            _key = jwtKeyHoldingService.JwtTokenKey;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var passwordHash = GetPasswordHash(password);
            User user = await _userRepository.GetUserByEmailAndPassword(email, password);
            if (user == null)
            {
                User toInsert = new User { Type = "Client", Email = email, Password = password }; //getPasswordHash(password)
                await _userRepository.InsertUser(toInsert);
                return true;
            }
            return false;
        }

        public async Task<string> AuthenticateUser(string email, string password)
        {
            var passwordHash = GetPasswordHash(password);
            User user = await _userRepository.GetUserByEmailAndPassword(email, password); //getPasswordHash(password)
            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            string claimType = (user.Type == "Administrator" ? CustomClaimTypes.Administrator : ClaimTypes.NameIdentifier);

            Claim[] claims = new Claim[] { new Claim(claimType, user.UserId.ToString(), user.Email) };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GetPasswordHash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<UserDto> GetCurrentUser(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return _mapper.Map<UserDto>(user);
        }
    }
}
