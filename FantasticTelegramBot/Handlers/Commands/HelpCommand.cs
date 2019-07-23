using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;

namespace FantasticTelegramBot.Handlers.Commands
{
    public class HelpCommand : CommandBase
    {
        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next, string[] args)
        {
            await context.Bot.Client.SendTextMessageAsync(context.Update.Message.From.Id, "some useful text");
        }
    }
}