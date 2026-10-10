using CommerceHub.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommerceHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // 1. Register all MediatR Commands, Queries, and Handlers in this assembly and pipeline behaviors
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            // 2. Automatically register all AbstractValidator classes found in this assembly
            services.AddValidatorsFromAssembly(assembly);

            // 3. Register AutoMapper using the configAction delegate
            services.AddAutoMapper(cfg =>
            {
                // This tells AutoMapper to scan the provided assembly for any classes 
                // that inherit from Profile (like your ProductMappingProfile)
                cfg.AddMaps(assembly);
            });

            return services;
        }
    }
}
