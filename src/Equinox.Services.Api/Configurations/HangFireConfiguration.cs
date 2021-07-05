using Equinox.Services.Api.Auth;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Equinox.Services.Api.Configurations
{
    public static class HangFireConfiguration
    {
        public static IServiceCollection AddHangfireConfig(this IServiceCollection services)
        {
            services.AddHangfire(c =>
            {
                c.UseSqlServerStorage("");
                c.UseSerializerSettings(new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
            });

            services.AddHangfireServer();

            return services;
        }

        public static IApplicationBuilder UseHangfireConfig(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard(pathMatch: "/hangfire", new DashboardOptions()
            {
                Authorization = new []{new AuthenticatedUserFilter()}
            });
            return app;
        }
    }
}