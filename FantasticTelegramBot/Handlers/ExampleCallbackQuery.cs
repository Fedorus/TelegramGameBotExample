using System.Threading;
using System.Threading.Tasks;
using FantasticTelegramBot.Models;
using Telegram.Bot.Framework.Abstractions;

namespace FantasticTelegramBot.Handlers
{
    public class ExampleCallbackQuery : IUpdateHandler
    {
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            var game = context.Items["game"] as GameObject;

            var callbackData = context.Update.CallbackQuery.Data;

            await context.Bot.Client.SendTextMessageAsync(context.Update.CallbackQuery.From.Id, callbackData, cancellationToken: cancellationToken);
        }
    }
}