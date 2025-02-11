using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SomeShop.Application.Interfaces;
using SomeShop.Application.Services;
using SomeShop.Application.Validators;
using SomeShop.Domain.Interfaces;

namespace SomeShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ProductValidator>();

            services.AddSingleton<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
