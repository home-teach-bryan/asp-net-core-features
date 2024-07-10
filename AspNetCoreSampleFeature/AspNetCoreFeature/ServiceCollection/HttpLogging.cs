using Microsoft.AspNetCore.HttpLogging;

namespace AspNetCoreFeature.ServiceCollection;

public static class HttpLogging
{
    public static IServiceCollection AddCustomHttpLogging(this IServiceCollection service)
    {
        service.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.RequestBody |
                                    HttpLoggingFields.ResponseBody |
                                    HttpLoggingFields.RequestHeaders |
                                    HttpLoggingFields.RequestPath |
                                    HttpLoggingFields.ResponseStatusCode;
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
            logging.CombineLogs = true;
        });
        return service;
    }
}