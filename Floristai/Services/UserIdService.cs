using Floristai.Repositories;
using Floristai.Models;
using System.Security.Claims;

namespace Floristai.Services
{
    public class UserIdService : IUserIdService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public UserIdService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserID()
        {
            var x = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == CustomClaimTypes.Administrator);
            return int.Parse(x.Value);
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        }

    }
}
