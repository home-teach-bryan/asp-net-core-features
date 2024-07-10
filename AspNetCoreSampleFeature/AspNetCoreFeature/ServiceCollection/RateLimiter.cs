using System.Threading.RateLimiting;

namespace AspNetCoreFeature.ServiceCollection;

public static class RateLimiter
{
    public static IServiceCollection AddCustomRateLimiter(this IServiceCollection service)
    {
        service.AddRateLimiter(options =>
        {
            options.OnRejected = (context, cancellationToken) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter = retryAfter.ToString();
                }
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.WriteAsJsonAsync(new { message = "Too many requests" });
                return ValueTask.CompletedTask;
            };
            options.AddPolicy("FixedWindows", httpContext => RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ??
                              httpContext.Request.Headers.Host.ToString(),
                factory: _ => new FixedWindowRateLimiterOptions()
                {
                    PermitLimit = 5,
                    Window = TimeSpan.FromSeconds(15),
                    //options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; //若達到限速條件時 後面進來的Request排入佇列 並決定從哪邊處理
                    //options.QueueLimit = 0; // 可進佇列等待的要求數    
                }
            ));
        });
        return service;
    }
}