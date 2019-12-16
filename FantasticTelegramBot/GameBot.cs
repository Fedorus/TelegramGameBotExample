using System;
using Telegram.Bot;
using Telegram.Bot.Framework;

namespace FantasticTelegramBot
{
    public class GameBot : BotBase
    {
        public GameBot(ITelegramBotClient client) : base(client.GetMeAsync().GetAwaiter().GetResult().Username, client)
        {
            Console.WriteLine(base.Username);
        }
    }
}