using System;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.Interactions;
using WujuStyleBot.BotServices.Command;
using WujuStyleBot.BotServices.Event;
using WujuStyleBot.BotServices.Event.Events;
using WujuStyleBot.BotServices.WujuVoice;

namespace WujuStyleBot.Modules
{
    public class WujuSlashCommands : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private CommandInteractionHandler _interactionHandler;
        private readonly IQuoteVoiceService _quoteVoiceService;

        public WujuSlashCommands(
            CommandInteractionHandler interactionHandler,
            IQuoteVoiceService quoteVoiceService)
        {
            _interactionHandler = interactionHandler;
            _quoteVoiceService = quoteVoiceService;
        }

        
        [SlashCommand("цитата", "случайная цитата Мастера Йи")]
        private async Task WujuRandomQuote()
        {
            await _quoteVoiceService
                .RandomQuote(
                    await GetAudioClient(), GetUserChannel());
        }
        
        [SlashCommand("наставление", "наставление от Мастера Йи")]
        private async Task WujuWiseQuote()
        {
            await _quoteVoiceService
                .RandomWiseQuote(
                    await GetAudioClient(), GetUserChannel());
        }
        
        [SlashCommand("смех", "смех Мастера")]
        private async Task WujuLaugh()
        {
            await _quoteVoiceService
                .WujuLaugh(
                    await GetAudioClient(), GetUserChannel());
        }

        [SlashCommand("вуджу", "призвать вуджу в войс", runMode: RunMode.Async)]
        public async Task WujuJoinChannel()
        {
            var channel = GetUserChannel();
            
            if (channel == null)
            {
                await Context.Channel
                    .SendMessageAsync
                        ("ВРОДЕ БЫ УЧЕНИК НО СНАЧАЛА НЕ ДОГАДАЛСЯ В ВОЙС ЗАЙТИ?");
            }
            
            var audioClient = await GetAudioClient();
            
            await _quoteVoiceService
                .SummonWuju(audioClient, channel);
        }

        private async Task<IAudioClient> GetAudioClient()
        {
              return await GetUserChannel().ConnectAsync(selfDeaf: true);
        }

        private IVoiceChannel GetUserChannel()
        {
            return (Context.User as IGuildUser)?.VoiceChannel;
        }
    }
}