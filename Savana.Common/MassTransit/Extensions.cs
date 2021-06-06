using System.Linq;
using System.Reflection;
using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Savana.Common.Errors;
using Savana.Common.Interfaces;
using Savana.Common.Settings;

namespace Savana.Common.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetEntryAssembly());
                configure.UsingRabbitMq((context, configurator) =>
                {
                    var config = context.GetService<IConfiguration>();
                    var serviceSettings = config.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    var rabbitMqSettings = config.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
                    configurator.Host(rabbitMqSettings.Host);
                    configurator.ConfigureEndpoints(context,
                        new SnakeCaseEndpointNameFormatter(serviceSettings.ServiceName, false)
                    );
                });
            }).AddMassTransitHostedService();
            return services;
        }

        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<,>));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();
                    var errorResponse = new ApiValidationErrorResponse {Errors = errors};
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}