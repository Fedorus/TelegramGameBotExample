using System.Threading;
using System.Threading.Tasks;
using FantasticTelegramBot.Exceptions;
using FantasticTelegramBot.Models;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace FantasticTelegramBot.Handlers
{
    internal class QuestExampleHandler : IUpdateHandler
    {
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            var game = context.Items["game"] as GameObject;

            var callbackData = context.Update.CallbackQuery.Data;

            int num = int.Parse(callbackData);
            
            await context.Bot.Client.AnswerCallbackQueryAsync(context.Update.CallbackQuery.Id, cancellationToken: cancellationToken);
            switch (num)
            {
                case 100:
                    game.User.GameProfile.GameState = 101;
                    await context.Bot.Client.SendTextMessageAsync(context.Update.CallbackQuery.From.Id, "Some very important quest text",
                        replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton(){ CallbackData = "101", Text = "Next"}), cancellationToken: cancellationToken);
                    break;
                case 101:
                    game.User.GameProfile.GameState = 0;
                    await context.Bot.Client.SendTextMessageAsync(context.Update.CallbackQuery.From.Id, "Quest ends here", cancellationToken: cancellationToken);
                    break;
                default:
                    throw new GameException("Unknown quest (ExampleQuest) state");
            }
        }
    }
}