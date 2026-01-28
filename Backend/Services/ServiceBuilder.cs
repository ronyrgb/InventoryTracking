using Backend.Repositories.Interfaces;
using Backend.Repositories;
using Backend.Services.Interfaces;
using Backend.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Services
{
    public static class ServiceBuilder
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductLoanService, ProductLoanService>();
            
            return services;
        }
    }
}
