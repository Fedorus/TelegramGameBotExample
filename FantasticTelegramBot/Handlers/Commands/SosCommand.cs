using System.Threading.Tasks;
using FantasticTelegramBot.Models;
using Telegram.Bot.Framework.Abstractions;

namespace FantasticTelegramBot.Handlers.Commands
{
    public class SosCommand : CommandBase
    {
        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next, string[] args)
        {
            var game = context.Items["game"] as GameObject;
            game.User.GameProfile.GameState = 0; // sets it to some neutral state ?
            await context.Bot.Client.SendTextMessageAsync(context.Update.Message.From.Id, "Done you are freee");
        }
    }
}