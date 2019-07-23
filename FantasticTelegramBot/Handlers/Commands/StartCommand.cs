using System.Threading.Tasks;
using FantasticTelegramBot.Models;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace FantasticTelegramBot.Handlers.Commands
{
    public class StartCommand : CommandBase
    {
        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next, string[] args)
        {
            var game = context.Items["game"] as GameObject;
            game.User.State = UserState.Good;
            await context.Bot.Client.SendTextMessageAsync(context.Update.Message.From.Id, "Received start command with args: "+string.Concat("", args),
                replyMarkup: new ReplyKeyboardMarkup(new KeyboardButton[]
                {
                    new KeyboardButton("📕 Quest line test"),
                    new KeyboardButton("📔 Set name"),
                }));
        }
    }
}