using System;
using System.Threading;
using System.Threading.Tasks;
using FantasticTelegramBot.Models;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.InlineQueryResults;

namespace FantasticTelegramBot.Handlers
{
    public class InlineQueryResolver : IUpdateHandler
    {
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            var game = context.Items["game"] as GameObject;
            Console.WriteLine(game.User.GameProfile.Name);
            
            await context.Bot.Client.AnswerInlineQueryAsync(context.Update.InlineQuery.Id,
                new InlineQueryResultBase[1]
                {
                    new InlineQueryResultArticle("Profile", "Profile", new InputTextMessageContent("Just an example"))
                }, cancellationToken: cancellationToken);
        }
    }
}