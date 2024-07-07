using System.Reflection;
using AspNetCoreFeature.Services;
using Microsoft.AspNetCore.Mvc;
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
        builder.Services.AddSingleton<ISchoolService, SchoolService>();
        builder.Services.AddSingleton<IClassRoomService, ClassRoomService>();
        builder.Services.AddSwaggerGen(item =>
        {
            item.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "學校管理系統API",
                Description = "學校管理系統API",
            });
            var xmlFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            item.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}