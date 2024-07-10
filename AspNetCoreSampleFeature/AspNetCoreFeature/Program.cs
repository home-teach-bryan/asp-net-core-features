using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.RateLimiting;
using AspNetCoreFeature.Jwt;
using AspNetCoreFeature.Services;
using AspNetCoreSample.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AspNetCoreFeature;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(item =>
        {
            item.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "產品管理系統API",
                Description = "產品管理系統API",
            });
            var xmlFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            item.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

            item.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme."
            });

            var requirement = new OpenApiSecurityRequirement();
            requirement.Add(new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme, Id = "bearerAuth"
                    }
                },
                new string[] { }
            );
                
            item.AddSecurityRequirement(requirement);
        });
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<JwtTokenGenerator>();
        

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SignKey"))),
                    ClockSkew = TimeSpan.Zero
                    
                };
            });

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddRateLimiter(options =>
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
        var app = builder.Build();

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