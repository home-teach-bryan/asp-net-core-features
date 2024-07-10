using System.Reflection;
using System.Text;
using AspNetCoreFeature.Jwt;
using AspNetCoreFeature.Services;
using AspNetCoreSample.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
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
                            Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SignKey")))
                };
            });


        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
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
        
        
        app.MapControllers();
        app.Run();
    }
}