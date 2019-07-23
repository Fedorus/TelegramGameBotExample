using Telegram.Bot.Framework.Abstractions;

namespace FantasticTelegramBot
{
    public static class WhenKeyBoardButton
    {
        public static bool ExampleButton(IUpdateContext obj)
        {
            return obj.Update.Message.Text.StartsWith("Emoji :)"); // just example :) all keyboardButtons should start with emoji 
        }
    }
}