using System.Threading.Tasks;
using Discord;
using Discord.Audio;

namespace WujuStyleBot.BotServices.WujuVoice
{
    public interface IQuoteVoiceService
    {
        Task RandomQuote(IAudioClient audioClient,  IVoiceChannel voiceChannel);

        Task RandomWiseQuote(IAudioClient audioClient, IVoiceChannel voiceChannel);

        Task SummonWuju(IAudioClient audioClient, IVoiceChannel voiceChannel);

        Task WujuLaugh(IAudioClient audioClient, IVoiceChannel voiceChannel);
    }
}

