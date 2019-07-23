using Telegram.Bot.Framework.Abstractions;

namespace FantasticTelegramBot
{
    public static class WhenCallbackQuery
    {
        public static bool Example(IUpdateContext obj)
        {
            return obj.Update.CallbackQuery.Data == "test";
        }
    }
}