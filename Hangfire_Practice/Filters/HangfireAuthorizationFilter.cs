using Hangfire.Dashboard;

namespace Hangfire_Practice.Filters;

public class HangfireAuthorizationFilter(IHostEnvironment env) : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        if (env.IsProduction())
        {
            var user = httpContext.User;
            return user.Identity?.IsAuthenticated == true &&
                    user.IsInRole("Admin");
        }
        else
        {
            return httpContext.Request.Host.Host == "localhost";
        }      
    }
}
