using Floristai.Repositories;
using Floristai.Services;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;

namespace Floristai.Middleware
{
    public class LoggingMiddleware
    {

        private RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoggingService _loggingService, IUserService _userService)
        {
            await _next(context);

            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<LoggingAttribute>();
            if (attribute != null)
            {
                var id = _userService.getCurrentUserId();
                var user = _userService.GetCurrentUser();
                
                var registered = await _loggingService.AddNewLogging(id.ToString(), user.ToString(), endpoint.ToString() );
            }
        }
    }
}
