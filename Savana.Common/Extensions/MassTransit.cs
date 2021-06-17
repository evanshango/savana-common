using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Savana.Common.Extensions
{
    public static class MassTransit
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetExecutingAssembly());
            }).AddMassTransitHostedService();
            return services;
        }
    }
}