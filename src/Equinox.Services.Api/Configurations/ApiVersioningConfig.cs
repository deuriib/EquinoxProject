using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Equinox.Services.Api.Configurations
{
    public static class ApiVersioningConfig
    {
        public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");

                config.Conventions.Add(new VersionByNamespaceConvention());
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            return services;
        }
    }
}
