using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Repositories
{
    public static class RepositoryBuilder
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Cada repositório específico
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductLoanRepository, ProductLoanRepository>();

            return services;
        }
    }
}
