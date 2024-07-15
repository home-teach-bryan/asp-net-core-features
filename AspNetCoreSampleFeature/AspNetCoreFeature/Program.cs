using AspNetCoreFeature.ActionFilter;
using AspNetCoreFeature.Jwt;
using AspNetCoreFeature.Middleware;
using AspNetCoreFeature.ServiceCollection;
using AspNetCoreFeature.Services;
using AspNetCoreSample.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AspNetCoreFeature;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        try
        {
            Log.Information("Starting Web Application");
            
            // Add services to the container.
            builder.Services.AddControllers(option =>
            {
                option.Filters.Add<ValidationModelActionFilter>();
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
            // logging
            builder.Services.AddSerilog();

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCustomHealthCheck();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseMiddleware<HttpLoggingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRateLimiter();
            app.MapControllers();
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application  terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}