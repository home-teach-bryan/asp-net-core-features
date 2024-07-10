using System.Reflection;
using Microsoft.OpenApi.Models;

namespace AspNetCoreFeature.ServiceCollection;

public static class SwaggerGen
{
    public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(item =>
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
        return services;
    }
}