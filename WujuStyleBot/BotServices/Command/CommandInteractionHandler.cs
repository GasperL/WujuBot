using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace WujuStyleBot.BotServices.Command
{
    public class CommandInteractionHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;
        private readonly IServiceProvider _services;
        
        public CommandInteractionHandler(
            DiscordSocketClient client, 
            InteractionService commands, 
            IServiceProvider services)
        {
            _client = client;
            _commands = commands;
            _services = services;
        }

        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            _client.InteractionCreated += HandleInteraction;

            _commands.SlashCommandExecuted += SlashCommandExecuted;
            _commands.ContextCommandExecuted += ContextCommandExecuted;
            _commands.ComponentCommandExecuted += ComponentCommandExecuted;
        }

        private Task ComponentCommandExecuted(
            ComponentCommandInfo componentCommandInfo,
            IInteractionContext interactionContext,
            IResult result)
        {
            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        break;
                    case InteractionCommandError.UnknownCommand:
                        break;
                    case InteractionCommandError.BadArgs:
                        break;
                    case InteractionCommandError.Exception:
                        break;
                    case InteractionCommandError.Unsuccessful:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private Task ContextCommandExecuted(
            ContextCommandInfo componentCommandInfo,
            IInteractionContext interactionContext,
            IResult result)
        {
            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        break;
                    case InteractionCommandError.UnknownCommand:
                        break;
                    case InteractionCommandError.BadArgs:
                        break;
                    case InteractionCommandError.Exception:
                        break;
                    case InteractionCommandError.Unsuccessful:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private Task SlashCommandExecuted(
            SlashCommandInfo slashCommandInfo,
            IInteractionContext interactionContext,
            IResult result)
        {
            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        break;
                    case InteractionCommandError.UnknownCommand:
                        break;
                    case InteractionCommandError.BadArgs:
                        break;
                    case InteractionCommandError.Exception:
                        break;
                    case InteractionCommandError.Unsuccessful:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private async Task HandleInteraction(SocketInteraction socketInteraction)
        {
            try
            {
                var socketInteractionContext = new SocketInteractionContext(_client, socketInteraction);
                await _commands.ExecuteCommandAsync(socketInteractionContext, _services);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

                if (socketInteraction.Type == InteractionType.ApplicationCommand)
                {
                    await socketInteraction
                        .GetOriginalResponseAsync()
                        .ContinueWith(async (message)
                            => await message.Result.DeleteAsync());
                }
            }
        }
    }
}