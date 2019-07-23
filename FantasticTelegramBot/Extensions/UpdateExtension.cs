using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FantasticTelegramBot.Extensions
{
    public static class UpdateExtension
    {
        public static bool StartsWithEmoji(this string str)
        {
            return !string.IsNullOrWhiteSpace(str) && str[0] > 3000;
        }

        public static long GetResponseId(this Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return update.Message.From.Id;
                case UpdateType.CallbackQuery:
                    return update.CallbackQuery.From.Id;
                case UpdateType.InlineQuery:
                    return update.InlineQuery.From.Id;
            }
            return 0;
        }
    }
}