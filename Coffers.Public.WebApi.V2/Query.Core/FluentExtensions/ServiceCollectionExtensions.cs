using System;
using Microsoft.Extensions.DependencyInjection;

namespace Query.Core.FluentExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegQueryProcessor(this IServiceCollection services, Action<QueryHandlerRegistry> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var queryHandlerRegistry = new QueryHandlerRegistry();
            action(queryHandlerRegistry);

            foreach (var registeredHandler in queryHandlerRegistry.RegisteredHandlers)
            {
                services.AddScoped(registeredHandler);
            }

            services.AddSingleton<IQueryHandlerRegistry>(queryHandlerRegistry);
            services.AddScoped<IQueryProcessor, QueryProcessor>();
        }
    }
}