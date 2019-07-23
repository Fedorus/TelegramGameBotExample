using System;
using System.Threading;
using System.Threading.Tasks;
using FantasticTelegramBot.Exceptions;
using FantasticTelegramBot.Extensions;
using Telegram.Bot.Framework.Abstractions;

namespace FantasticTelegramBot.Handlers
{
    internal class ExceptionHandler : IUpdateHandler
    {
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            try
            {
                await next(context, cancellationToken);
            }
            catch (GameException ex)
            {
                try
                {
                    await context.Bot.Client.SendTextMessageAsync(context.Update.GetResponseId(), ex.Message, cancellationToken: cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // more logging
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // more logging
            }
        }
    }
}