using System;
using MongoDB.Bson;
using Telegram.Bot.Types;

namespace FantasticTelegramBot.Models
{
    public class TelegramUser
    {
        public long Id { get; set; }
        public User UserData { get; set; }
        public int MessageCounter { get; set; }
        public ObjectId? LastMessage { get; set; }
        public UserState State { get; set; }
        public GameProfile GameProfile { get; set; } = new GameProfile();

        public string BanReason { get; set; }
        public DateTime FirstSeen { get; set; }
    }

    public class GameProfile
    {
        /// <summary>
        /// All states that connected with input or game expectations.
        /// In range 0..50 its some common game states
        /// In range 50..100 its waiting for user description inputs.
        /// if bigger then 100 its game dialogs
        /// </summary>
        public int GameState { get; set; } = 0;
        public int Level { get; set; }
        public int Money { get; set; }
        public string Name { get; set; }
    }
}