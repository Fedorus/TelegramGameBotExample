using System.Threading;
using System.Threading.Tasks;
using FantasticTelegramBot.Models;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace FantasticTelegramBot.Handlers
{
    public class ExampleButtonHandler : IUpdateHandler
    {
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            var game = context.Items["game"] as GameObject;
            
            if (context.Update.Message.Text.StartsWith("📕"))
            {
                game.User.GameProfile.GameState = 100;
                await context.Bot.Client.SendTextMessageAsync(context.Update.Message.From.Id, "This is quest starting message",
                    replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton(){ CallbackData = "100", Text = "Next"}), cancellationToken: cancellationToken);
            }
            else if (context.Update.Message.Text.StartsWith("📔"))
            {
                game.User.GameProfile.GameState = 50;
                await context.Bot.Client.SendTextMessageAsync(context.Update.Message.From.Id, "Input your name:", cancellationToken: cancellationToken);
            }
            else
            {
                await context.Bot.Client.SendTextMessageAsync(context.Update.Message.From.Id,"You pushed button:" + context.Update.Message.Text, cancellationToken: cancellationToken);
            }
        }
    }
}