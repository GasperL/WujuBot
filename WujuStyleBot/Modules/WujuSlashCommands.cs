using System;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using WujuStyleBot.BotServices.Command;
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
            var userChannel = await GetUserChannelAsync();

            await _quoteVoiceService
                .RandomQuote(
                      await userChannel.ConnectAsync(selfDeaf: true), userChannel);
        }

        [SlashCommand("наставление", "наставление от Мастера Йи")]
        private async Task WujuWiseQuote()
        {
            var userChannel = await GetUserChannelAsync();

            var quote = await _quoteVoiceService
                .RandomWiseQuote(
                    await userChannel.ConnectAsync(selfDeaf: true), userChannel);

            await Context.Interaction.FollowupAsync(quote);
        }

        [SlashCommand("смех", "смех Мастера")]
        private async Task WujuLaugh()
        {
            var userChannel = await GetUserChannelAsync();

            var quote = await _quoteVoiceService
                .WujuLaugh(
                   await userChannel.ConnectAsync(selfDeaf: true), userChannel);

            await Context.Interaction.FollowupAsync(quote);
        }

        [SlashCommand("вуджу", "призвать вуджу в войс", runMode: RunMode.Async)]
        private async Task WujuSummonChannel()
        {
            var userChannel = await GetUserChannelAsync();

            var quote = await _quoteVoiceService
               .SummonWuju(
                   await userChannel.ConnectAsync(selfDeaf: true), userChannel);

            await Context.Interaction.FollowupAsync(quote);
        }

        private async Task<IVoiceChannel> GetUserChannelAsync()
        {
            await RespondAsync("Думаю амм...");

            var voiceChannel = (Context.User as IGuildUser)?.VoiceChannel;

            return await VoiceChannelAssert(voiceChannel);
        }

        private async Task<IVoiceChannel> VoiceChannelAssert(IVoiceChannel voiceChannel)
        {
            if (voiceChannel == null)
            {
                await Context.Interaction
                    .FollowupAsync("ААААА!!! ТЫ ТУПОЙ УЧЕНИК! ЗАЙДИ В ВОЙС СНАЧАЛА!!!");

                throw new Exception("Voice channel doesen't exist.");
            }

            return voiceChannel;
        }
    }
}