using Application.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    /// <summary>
    /// Adds the application services to the service collection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add all the application services to a service collection.
        /// </summary>
        /// <param name="services">The service collection to add to.</param>
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<BadgeService>();
            services.AddScoped<LeagueService>();
            services.AddScoped<PointsService>();
            services.AddScoped<SessionService>();
            services.AddScoped<UserService>();
        }
    }
}
