using System.Security.Claims;

namespace Floristai.Services
{
    public class UserIdService : IUserIdService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserIdService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserID()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
