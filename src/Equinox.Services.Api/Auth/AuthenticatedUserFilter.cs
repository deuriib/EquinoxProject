using Hangfire.Dashboard;

namespace Equinox.Services.Api.Auth
{
    public class AuthenticatedUserFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return httpContext.User.Identity?.IsAuthenticated ?? false;
        }
    }
}