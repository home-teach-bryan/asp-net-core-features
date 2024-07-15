using System.Text.Json.Nodes;
using Serilog;

namespace AspNetCoreFeature.Middleware;

public class HttpLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDiagnosticContext _diagnosticContext;

    public HttpLoggingMiddleware(RequestDelegate next, IDiagnosticContext diagnosticContext)
    {
        _next = next;
        _diagnosticContext = diagnosticContext;
    }

    public async Task Invoke(HttpContext context)
    {
        var requestBodyStream = new MemoryStream();
        await context.Request.Body.CopyToAsync(requestBodyStream);
        requestBodyStream.Seek(0, SeekOrigin.Begin);

        using var requestStreamReader = new StreamReader(requestBodyStream);
        var requestBodyText = await requestStreamReader.ReadToEndAsync();

        if (context.Request.ContentType == "application/json")
        {
            requestBodyText = MakePasswordMasked(requestBodyText);
        }

        _diagnosticContext.Set("RequestBody", requestBodyText);
        requestBodyStream.Seek(0, SeekOrigin.Begin);
        context.Request.Body = requestBodyStream;

        var originalResponseBody = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        responseBody.Seek(0, SeekOrigin.Begin);
        using var responseStreamReader = new StreamReader(responseBody);
        var responseBodyText = await responseStreamReader.ReadToEndAsync();
        if (context.Request.Path == "/api/Token")
        {
            responseBodyText = MakeTokenMasked(responseBodyText);
        }
        _diagnosticContext.Set("ResponseBody", responseBodyText);
        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalResponseBody);
    }

    private string MakeTokenMasked(string responseBodyText)
    {
        var jsonNode = JsonNode.Parse(responseBodyText);
        if (jsonNode == null)
        {
            return responseBodyText;
        }
        if (jsonNode.AsObject().ContainsKey("data"))
        {
            jsonNode["data"] = "******";
        }
        return jsonNode.ToJsonString();
    }

    private string MakePasswordMasked(string requestBodyText)
    {
        var jsonNode = JsonNode.Parse(requestBodyText);
        if (jsonNode == null)
        {
            return requestBodyText;
        }
        if (jsonNode.AsObject().ContainsKey("password"))
        {
            jsonNode["password"] = "******";
        }
        return jsonNode.ToJsonString();
    }
}