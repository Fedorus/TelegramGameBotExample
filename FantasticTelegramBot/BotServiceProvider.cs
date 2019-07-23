using System;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Framework.Abstractions;

namespace FantasticTelegramBot
{
    public class BotServiceProvider : IBotServiceProvider
    {
        private readonly IServiceProvider _container;

        private readonly IServiceScope _scope;

        public BotServiceProvider(IServiceProvider provider)
        {
            _container = provider;
        }

        public BotServiceProvider(IServiceScope scope)
        {
            _scope = scope;
        }

        public object GetService(Type serviceType) =>
            _scope != null
                ? _scope.ServiceProvider.GetService(serviceType)
                : _container.GetService(serviceType);

        public IBotServiceProvider CreateScope() =>
            new BotServiceProvider(_container.CreateScope());

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}