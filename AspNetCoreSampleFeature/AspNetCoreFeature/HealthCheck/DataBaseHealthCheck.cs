using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AspNetCoreFeature.HealthCheck;

public class DataBaseHealthCheck : IHealthCheck
{
    private readonly string _connectionString;

    public DataBaseHealthCheck(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(HealthCheckResult.Healthy("The database is healthy"));
    }
}