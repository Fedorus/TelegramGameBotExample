using Telegram.Bot.Types;

namespace FantasticTelegramBot.Models
{
    public class GameObject
    {
        public TelegramUser User { get; set; }
        public TelegramUpdate CurrentUpdate { get; set; }
        public TelegramUpdate PreviousUpdate { get; set; }
    }
}