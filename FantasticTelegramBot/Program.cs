using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using System.Threading.Tasks;
using FantasticTelegramBot.Exceptions;
using FantasticTelegramBot.Extensions;
using FantasticTelegramBot.Handlers;
using FantasticTelegramBot.Handlers.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Telegram.Bot;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;

namespace FantasticTelegramBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var options = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build()
                          ?? throw new ArgumentNullException(
                              "new ConfigurationBuilder().AddJsonFile(\"appsettings.Development.json\").Build()");
            ServiceCollection services = new ServiceCollection();

            services.AddScoped<IMongoDatabase>(x =>
                new MongoClient(options["MongoDbConnectionString"]).GetDatabase("GameBot"));
            services.AddSingleton<ITelegramBotClient>(x =>
                new TelegramBotClient(options.GetSection("GameBot")["ApiToken"]));

            services.AddSingleton<GameBot>();
            services.AddScoped<ExceptionHandler>();
            services.AddScoped<GameObjectBuilder>();
            services.AddScoped<UserStateReaction>();
            services.AddScoped<StartCommand>();
            services.AddScoped<HelpCommand>();
            services.AddScoped<SosCommand>();
            services.AddScoped<InlineQueryResolver>();
            services.AddScoped<BasicTextInputsResolver>();
            services.AddScoped<QuestExampleHandler>();
            services.AddScoped<ExampleCallbackQuery>();
            services.AddScoped<ExampleButtonHandler>();

            var manager = new UpdatePollingManager<GameBot>(ConfigureBot(),
                new BotServiceProvider(services.BuildServiceProvider()));
            var requestFilter = new GetUpdatesRequest();
            requestFilter.AllowedUpdates = new[] {UpdateType.Message, UpdateType.CallbackQuery, UpdateType.InlineQuery};

            await manager.RunAsync();
        }

        private static IBotBuilder ConfigureBot()
        {
            return
                new BotBuilder()
                    .Use<ExceptionHandler>() // should catch all exceptions
                    .Use<GameObjectBuilder>() // now Items have "game" object which is GameObject 
                    .MapWhen<UserStateReaction>(When.UserRestricted) // if user blocked/restricted gives reaction 
                    
                    .MapWhen(When.MessageNotForwarded, filteredBranch => filteredBranch

                        .MapWhen(When.NewCommand, commandsBranch =>
                                commandsBranch // processes commands. ignores all other branches
                                    .UseCommand<StartCommand>("start") // processes first message to bot
                                    .UseCommand<HelpCommand>("help") // gives help to user
                                    .UseCommand<SosCommand>("sos") // used if user gets himself in invalid state4
                        )

                        .MapWhen<InlineQueryResolver>(When.InlineQuery) // resolves inline queries like "me" gives user`s game profile etc 

                        .MapWhen(When.GameStateNeedsResolve, dialogsBranch => dialogsBranch
                            .MapWhen<BasicTextInputsResolver>(When.BasicTextInputExpected)
                            //GameDialogResolver
                            .MapWhen(When.CallbackQuery, questsBranch => questsBranch
                                .Use<QuestExampleHandler>() //WhenQuest.QuestExample
                            )
                        )

                        .MapWhen(When.CallbackQuery, inlineButtonBranch =>
                            inlineButtonBranch // processes button connected with message click
                                .Use<ExampleCallbackQuery>() //WhenCallbackQuery.Example
                        )

                        .MapWhen(When.KeyboardButtonPressed, keyboardBranch =>
                            keyboardBranch // every button text should start with unique emoji
                                .Use<ExampleButtonHandler>()
                        )
                    )
                ;
        }
    }
}