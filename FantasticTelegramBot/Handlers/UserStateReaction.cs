using System;
using System.Threading;
using System.Threading.Tasks;
using FantasticTelegramBot.Models;
using Telegram.Bot.Framework.Abstractions;

namespace FantasticTelegramBot.Handlers
{
    public class UserStateReaction : IUpdateHandler
    {
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            var game = context.Items["game"] as GameObject;
            
            switch (game.User.State)
            {
                case UserState.Freshman:
                    Console.WriteLine("State is not initialised");
                    break;
                case UserState.Good:
                    Console.WriteLine("Should not be here");
                    break;
                case UserState.Blocked:
                    Console.WriteLine("User blocked");
                    break;
                case UserState.Restricted:
                    Console.WriteLine("User restricted");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}