using Floristai.Services;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;

namespace Floristai.Middleware
{
    public class LoggingMiddleware
    {
        private readonly IUserIdService _userIdService;
        private readonly ILoggingService _loggingService;

        private RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoggingService _loggingService, IUserIdService _userIdService)
        {
            await _next(context);

            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<LoggingAttribute>();
            var id = _userIdService.GetUserID();
            if (attribute != null)
            {
                var ev = attribute.Event;
                var registered = await _loggingService.AddNewLogging(id.ToString(), _userIdService.GetUserClaims(id).ToString(), endpoint.ToString() );
            }
        }
    }
}
