using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.Services;
using TechChallenge.Data.Contracts;
using TechChallenge.Infra;
using TechChallenge.Infra.Repositories;

namespace TechChallenge.Application
{
    public static class InjectionDependency
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<ICardService, CardServices>();

            return services;
        }
    }
}
