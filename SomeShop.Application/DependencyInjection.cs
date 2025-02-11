using SomeShop.Application.Services;
using SomeShop.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SomeShop.Application.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace SomeShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ProductValidator>();

            return services;
        }
    }
}
