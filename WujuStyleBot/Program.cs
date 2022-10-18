using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WujuStyleBot.BotServices.Command;
using WujuStyleBot.BotServices.Logger;
using WujuStyleBot.BotServices.WujuVoice;

namespace WujuStyleBot
{
    internal class Program
    {
        private IConfiguration _config;
        private DiscordSocketClient _client;
        private InteractionService _commands;

        public static Task Main(string[] args) => new Program().MainAsync();

        private async Task MainAsync()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json")
                .Build();

            await RunAsync(Host.CreateDefaultBuilder()
                .ConfigureServices((_, serviceCollection) =>
                    serviceCollection
                        .AddSingleton(_config)
                        .AddSingleton(_ => new DiscordSocketClient(new DiscordSocketConfig
                        {
                            GatewayIntents = GatewayIntents.AllUnprivileged,
                            LogGatewayIntentWarnings = false,
                            AlwaysDownloadUsers = true,
                            LogLevel = LogSeverity.Debug
                        }))
                        .AddTransient<ConsoleLogger>()
                        .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                        .AddSingleton<CommandInteractionHandler>()
                        .AddSingleton(_ => new CommandService(new CommandServiceConfig
                        {
                            LogLevel = LogSeverity.Debug,
                            DefaultRunMode = Discord.Commands.RunMode.Async
                        }))
                        .AddSingleton<IQuoteVoiceService, QuoteVoiceService>()
                        .AddSingleton<SocketSlashCommand>())
                .Build());
        }

        private async Task RunAsync(IHost host)
        {
            var provider = host.Services.CreateScope().ServiceProvider;

            await RegisterEntryServices(provider);

            _client.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);
            _commands.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);
            _client.Ready += ReadyAsync;
            
            await _client.LoginAsync(TokenType.Bot, _config["Token"]);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task ReadyAsync()
        {
            if (BoolHelpers.IsDebug())
            {
                await _commands.RegisterCommandsToGuildAsync(ulong.Parse(_config["TestGuildId"]!));
            }
            else
            {
                await _commands.RegisterCommandsGloballyAsync(deleteMissing: true);
            }
        }

        private async Task RegisterEntryServices(IServiceProvider provider)
        {
            _commands = provider.GetRequiredService<InteractionService>();
            _client = provider.GetRequiredService<DiscordSocketClient>();
            await provider.GetRequiredService<CommandInteractionHandler>().InitializeAsync();
        }
    }
}