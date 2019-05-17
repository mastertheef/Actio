﻿using Actio.Common.Commands;
using Actio.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe.Middleware;
using System.Reflection;
using System.Threading.Tasks;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, 
            ICommandHandler<TCommand> handler) where TCommand : ICommand
             => bus.SubscribeAsync<TCommand>(
                 msg => handler.HandleAsync(msg),
                 ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TCommand>())));

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler) where TEvent : IEvent
             => bus.SubscribeAsync<TEvent>(
                 msg => handler.HandleAsync(msg),
                 ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TEvent>())));

        public static void AddRabbitMq(this IServiceCollection service, IConfiguration config)
        {
            var options = new RabbitMqOptions();
            var section = config.GetSection("rabbitMq");
            section.Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });
            service.AddSingleton<IBusClient>(_ => client);
        }

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";


    }
}
