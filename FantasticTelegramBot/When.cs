using System.Linq;
using FantasticTelegramBot.Extensions;
using FantasticTelegramBot.Models;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.Enums;

namespace FantasticTelegramBot
{
    public static class When
    {
        public static bool InlineQuery(IUpdateContext obj)
        {
            return obj.Update.InlineQuery !=null;
        }

        public static bool KeyboardButtonPressed(IUpdateContext obj)
            => obj.Update.Type == UpdateType.Message && 
               obj.Update.Message.Text.StartsWithEmoji();
        
        public static bool NewCommand(IUpdateContext context) => 
            context.Update.Message?.Entities?.FirstOrDefault()?.Type == MessageEntityType.BotCommand;

        public static bool UserRestricted(IUpdateContext obj)
        {
            return obj.Items["game"] is GameObject gameObject && !(gameObject.User.State == UserState.Freshman || gameObject.User.State == UserState.Good); 
        }

        public static bool GameStateNeedsResolve(IUpdateContext obj)
        {
            return obj.Items["game"] is GameObject gameObject && gameObject.User.GameProfile.GameState >= 50;
        }

        public static bool BasicTextInputExpected(IUpdateContext obj)
        {
            return obj.Update.Message !=null && obj.Items["game"] is GameObject gameObject && gameObject.User.GameProfile.GameState >= 50 && gameObject.User.GameProfile.GameState < 100;
        }

        public static bool CallbackQuery(IUpdateContext obj) => 
            obj.Update.Type == UpdateType.CallbackQuery;

        public static bool MessageNotForwarded(IUpdateContext obj) =>
            obj.Update.Message == null || obj.Update.Message.ForwardFrom == null;
    }
    
    
}