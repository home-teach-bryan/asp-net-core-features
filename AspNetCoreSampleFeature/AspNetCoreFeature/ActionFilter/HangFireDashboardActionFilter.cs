using Hangfire.Dashboard;

namespace AspNetCoreFeature.ActionFilter;

public class HangFireDashboardActionFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}