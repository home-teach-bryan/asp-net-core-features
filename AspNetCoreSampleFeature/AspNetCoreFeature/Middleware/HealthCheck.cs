using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AspNetCoreFeature.Middleware;

public static class HealthCheck
{
    public static IEndpointRouteBuilder UseCustomHealthCheck(this IEndpointRouteBuilder app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = (httpContext, healthReport) =>
            {
                var result = new
                {
                    health = Enum.Parse<HealthStatus>(healthReport.Status.ToString()).ToString(),
                    services = healthReport.Entries.Select(item => new
                    {
                        name = item.Key,
                        health = Enum.Parse<HealthStatus>(item.Value.Status.ToString()).ToString()
                    })
                };
                httpContext.Response.ContentType = "application/json";
                var json = System.Text.Json.JsonSerializer.Serialize(result);
                return httpContext.Response.WriteAsync(json);
            }
        });
        return app;
    }
}