using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Validators
{
    public static class ValidatorBuilder
    {
        
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductLoanCreateValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductLoanUpdateValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductValidator>();
            

            return services;
        }
    }
}
