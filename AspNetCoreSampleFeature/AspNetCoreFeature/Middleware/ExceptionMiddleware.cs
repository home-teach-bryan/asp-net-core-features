using AspNetCoreFeature.Extension;
using AspNetCoreSample.Models.Enum;
using AspNetCoreSample.Models.Response;

namespace AspNetCoreFeature.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new ApiResponse<object>()
        {
            Status = ApiResponseStatus.Fail,
            Message = ApiResponseStatus.Fail.GetDescription(),
            Errors = new List<string>() { exception.Message },
            Data = null,
        };
        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}