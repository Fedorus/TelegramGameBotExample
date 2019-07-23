using System.Threading;
using System.Threading.Tasks;
using FantasticTelegramBot.Exceptions;
using FantasticTelegramBot.Models;
using Telegram.Bot.Framework.Abstractions;

namespace FantasticTelegramBot.Handlers
{
    public class BasicTextInputsResolver : IUpdateHandler
    {
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            //we expecting 
            if (context.Update.Message == null)
                throw new GameException("Exception message");
                
            var game = context.Items["game"] as GameObject;
            if ( 50 <= game.User.GameProfile.GameState && game.User.GameProfile.GameState < 100 )
            {
                game.User.GameProfile.GameState = 0;
                game.User.GameProfile.Name = context.Update.Message.Text;
            }
        }
    }
}