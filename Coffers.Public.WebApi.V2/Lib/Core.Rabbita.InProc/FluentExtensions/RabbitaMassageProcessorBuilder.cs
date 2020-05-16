using System;
using Core.Rabbita.Core;
using Core.Rabbita.FluentExtensions;
using Core.Rabbita.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Rabbita.InProc.FluentExtensions
{
    public static class RabbitaMassageProcessorBuilder
    {
        public static void AddEventProcessor(this IServiceCollection services, Action<IEventHandlerRegistry> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var queryHandlerRegistry = new EventHandlerRegistry();
            action(queryHandlerRegistry);

            foreach (var registeredHandler in queryHandlerRegistry.RegisteredHandlers){
                services.AddScoped(registeredHandler);
            }

            services.AddSingleton<IEventHandlerRegistry>(queryHandlerRegistry);
            services.AddHostedService<InternalEventProcessor>();
        }
        /*
           public static void AddCommandProcessor(this IServiceCollection services, Action<CommandHandlerRegistry> action)
           {
               if (action == null)
                   throw new ArgumentNullException(nameof(action));
               services.AddSingleton<InProcMessageDispatchers<ICommand>>();
               services.AddHostedService<InternalMessageProcessor<ICommand>>();
           }  
        */

        public static void AddEventBus(this IServiceCollection services)
        {
            services.AddSingleton<AsyncConcurrentQueue<IEvent>>();
            services.AddScoped<IEventBus, InProcEventBus>();
        }

        public static void AddCommandBus(this IServiceCollection services)
        {
            services.AddSingleton<AsyncConcurrentQueue<ICommand>>();
            services.AddScoped<ICommandBus, InProcCommandBus>();
        }
    }
}