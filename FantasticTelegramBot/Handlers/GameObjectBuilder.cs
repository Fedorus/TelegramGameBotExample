using System;
using System.Threading;
using System.Threading.Tasks;
using FantasticTelegramBot.Models;
using MongoDB.Driver;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace FantasticTelegramBot.Handlers
{
    public class GameObjectBuilder : IUpdateHandler
    {
        private IMongoCollection<TelegramUser> _users;
        private IMongoCollection<TelegramUpdate> _logs;

        public GameObjectBuilder(IMongoDatabase db)
        {
            _logs = db.GetCollection<TelegramUpdate>("Updates");
            _users = db.GetCollection<TelegramUser>("Users");
        }

        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            //Logs update message
            var update = new TelegramUpdate() {Update = context.Update};
            await _logs.InsertOneAsync(update, cancellationToken: cancellationToken);
            
            //Gets or creates user 
            var user = GetUser(context.Update);
            TelegramUser telegramUser = (await _users
                .FindAsync(x => x.Id == user.Id, cancellationToken: cancellationToken)).FirstOrDefault();
            if (telegramUser == null)
            {
                telegramUser = new TelegramUser()
                    {UserData = user, Id = user.Id, FirstSeen = DateTime.Now};
                await _users.InsertOneAsync(telegramUser, cancellationToken: cancellationToken);
            }

            var gameObject = new GameObject();
            gameObject.User = telegramUser;
            if (telegramUser.LastMessage != null)
            {
                gameObject.PreviousUpdate = (await _logs.FindAsync(x => x.Id == telegramUser.LastMessage, cancellationToken: cancellationToken)).FirstOrDefault();
            }
            context.Items.Add("game", gameObject);
            
            try
            {
                await next(context, cancellationToken);
            }
            finally
            {
                Console.WriteLine("Saving User");
                telegramUser.LastMessage = update.Id;
                await _users.FindOneAndReplaceAsync(x => x.Id == telegramUser.Id, telegramUser, cancellationToken: cancellationToken);
            }
        }
        
        private User GetUser(Update update)
        {
            User user = null;
            if (update.Message != null)
            {
                user = update.Message.From;
            }
            else if(update.CallbackQuery !=null)
            {
                user = update.CallbackQuery.From;
            }
            else if (update.InlineQuery != null)
            {
                user = update.InlineQuery.From;
            }
            else
            {
                throw new Exception($"Can`t get user`s profile from message type: {update.Type.ToString()}");
            }

            return user;
        }
    }
}