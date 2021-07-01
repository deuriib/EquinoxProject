﻿using Equinox.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Equinox.Services.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);

            return services;
        }
    }
}