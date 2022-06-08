using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;

namespace WujuStyleBot.BotServices.WujuVoice
{
    public class QuoteVoiceService : IQuoteVoiceService
    {
        public async Task RandomQuote(IAudioClient audioClient, IVoiceChannel voiceChannel)
        {
            await VoiceService.SendAsync(audioClient, voiceChannel, GetQuote("AllQuotes"));
        }

        public async Task RandomWiseQuote(IAudioClient audioClient, IVoiceChannel voiceChannel)
        {
            await VoiceService.SendAsync(audioClient, voiceChannel, GetQuote("WiseQuotes"));
        }

        public async Task SummonWuju(IAudioClient audioClient, IVoiceChannel voiceChannel)
        {
            await VoiceService.SendAsync(audioClient, voiceChannel, "./sound/Quotes/AllQuotes/я-умирать.ogg");
        }

        public async Task WujuLaugh(IAudioClient audioClient, IVoiceChannel voiceChannel)
        {
            await VoiceService.SendAsync(audioClient, voiceChannel, GetQuote("Laugh"));
        }

        private static string GetQuote(string quotesDirectory)
        {
            var random = new Random();
            var allFilesName = Directory.GetFiles($"./sound/Quotes/{quotesDirectory}", "*.ogg");
            
            return allFilesName[random.Next(allFilesName.Length)];
        }
    }
}