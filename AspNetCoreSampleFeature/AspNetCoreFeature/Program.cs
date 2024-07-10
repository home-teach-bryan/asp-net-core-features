using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.RateLimiting;
using AspNetCoreFeature.ActionFilter;
using AspNetCoreFeature.HealthCheck;
using AspNetCoreFeature.Jwt;
using AspNetCoreFeature.ServiceCollection;
using AspNetCoreFeature.Services;
using AspNetCoreSample.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AspNetCoreFeature;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(option =>
        {
            option.Filters.Add<ApiResponseActionFilter>();
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.Configure<ApiBehaviorOptions>(item => item.SuppressModelStateInvalidFilter = true);
        // swagger document spec 
        builder.Services.AddCustomSwaggerGen();
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<JwtTokenGenerator>();
        
        // jwt authentication setting
        builder.Services.AddCustomJwtAuthentication(builder.Configuration);

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        // rate limit
        builder.Services.AddCustomRateLimiter();

        // health check
        builder.Services.AddCustomHealthCheck();
        var app = builder.Build();
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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRateLimiter();

        app.MapControllers();
        app.Run();
    }
}