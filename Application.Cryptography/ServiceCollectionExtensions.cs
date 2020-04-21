using Application.Cryptography.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Application.Cryptography
{
    /// <summary>
    /// Adds the cryptography services to the service collection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add cryptography services to a service collection.
        /// </summary>
        /// <param name="services">The service collection to add to.</param>
        public static void AddCryptoServices(this IServiceCollection services)
        {
            services.AddSingleton<CryptoService>();
        }
    }
}
