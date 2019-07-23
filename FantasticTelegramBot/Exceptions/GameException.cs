using System;

namespace FantasticTelegramBot.Exceptions
{
    public class GameException : Exception
    {
        public GameException(string message) : base(message)
        {
        }
    }
}