using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Equinox.Services.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            return services;
        }

        public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var api in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{api.GroupName}/swagger.json", api.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var api in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(api.GroupName, new OpenApiInfo
                {
                    Version = api.GroupName,
                    Title = "Equinox Project",
                    Description = api.IsDeprecated ? "(DEPRECATED) Equinox API Swagger surface" : "Equinox API Swagger surface",
                    Contact = new OpenApiContact { Name = "Deuri Vasquez", Email = "deuriib@gmail.com", Url = new Uri("https://github.com/deuriib") },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://github.com/deuriib/EquinoxProject/blob/master/LICENSE") }
                });
            }

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Input the JWT like: Bearer {your token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        }
    }
}