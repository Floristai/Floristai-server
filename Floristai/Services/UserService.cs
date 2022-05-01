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
        private readonly string key;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this.key = "aaaa[aa]aa[aa]aa";
        }
        public async Task<bool> RegisterUser(string email, string password)
        {
            if (_userRepository.GetUserId(email, password) == 0)
            {
                await _userRepository.InsertUser(email, getPasswordHash(password));
                return true;
            }
            return false;
        }

        public async Task<string> AuthenticateUser(string email, string password)
        {
            int userID = _userRepository.GetUserId(email, getPasswordHash(password));
            if (userID == 0) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            Claim[] claims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, userID.ToString()) };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string getPasswordHash(string password)
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

       
    }
}
