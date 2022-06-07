using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.Interactions;
using WujuStyleBot.BotServices.Command;
using WujuStyleBot.BotServices.Event;
using WujuStyleBot.BotServices.Event.Events;

namespace WujuStyleBot.Modules
{
    public class WujuSlashCommands : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private CommandInteractionHandler _interactionHandler;
        private readonly IEventService _eventService;

        public WujuSlashCommands(
            CommandInteractionHandler interactionHandler,
            IEventService eventService)
        {
            _interactionHandler = interactionHandler;
            _eventService = eventService;
        }

        [SlashCommand("событиее", "назначить событие")]
        private async Task WujuEventHandler(string name, string date)
        {
            await _eventService.RegisterEvent(new DeferredEvent
            {
                Name = name,
                StartAt = DateTime.Parse(date),
                DiscordInteraction = Context.Interaction
            });

            _eventService.Start();

            await RespondAsync($" Событие {name} началось");
        }


        [SlashCommand("вуджу", "призвать вуджу в войс", runMode: RunMode.Async)]
        public async Task JoinChannel()
        {
            var channel = (Context.User as IGuildUser)?.VoiceChannel;

            if (channel == null)
            {
                await Context.Channel
                    .SendMessageAsync
                        ("ВРОДЕ БЫ УЧЕНИК НО ДОГАДАЛСЯ ЗАЙТИ В ВОЙС СНАЧАЛА?");
                return;
            }
            
            
            var audioClient = await channel.ConnectAsync(selfDeaf: true);
            
            await SendAsync(audioClient, "C:\\Users\\User\\Music\\ПАСТУХ - Я УМИРАТЬ.mp3", channel);
        }

        private Process CreateStream(string path)
        {
            var process =  Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true
            });
            
            return process;
        }
        
        private async Task SendAsync(IAudioClient client, string path, IVoiceChannel channel)
        {
            using var ffmpeg = CreateStream(path);
            using var output = ffmpeg.StandardOutput.BaseStream;
            using var discord = client.CreatePCMStream(AudioApplication.Mixed);
        
            try
            {
                await output.CopyToAsync(discord);
            }
            finally
            {
                await discord.FlushAsync();
                await channel.DisconnectAsync();
            }
        }
    }
}