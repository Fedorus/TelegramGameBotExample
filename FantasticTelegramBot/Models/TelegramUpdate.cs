using MongoDB.Bson;
using Telegram.Bot.Types;
using Microsoft.Extensions.Logging;

namespace FantasticTelegramBot.Models
{
    public class TelegramUpdate
    {
        public ObjectId Id { get; set; }
        public Update Update { get; set; }
        public LogLevel Level { get; set; } = LogLevel.Information;
    }
}