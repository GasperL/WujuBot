using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.Interactions;

namespace WujuStyleBot.BotServices.WujuVoice
{
    public interface IQuoteVoiceService
    {
        Task<string> RandomQuote(IAudioClient audioClient,  IVoiceChannel voiceChannel);

        Task<string> RandomWiseQuote(IAudioClient audioClient, IVoiceChannel voiceChannel);

        Task<string> SummonWuju(IAudioClient audioClient, IVoiceChannel voiceChannel);

        Task<string> WujuLaugh(IAudioClient audioClient, IVoiceChannel voiceChannel);
    }
}

