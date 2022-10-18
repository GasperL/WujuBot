using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;

namespace WujuStyleBot.BotServices.WujuVoice
{
    public class QuoteVoiceService : IQuoteVoiceService
    {
        public async Task<string> RandomQuote(
            IAudioClient audioClient,
            IVoiceChannel voiceChannel)
        {
            var quotePath = GetRandomQuotePath("AllQuotes");
            var quoteText = GetTextOfQuote(quotePath);

            await VoiceService.SendAsync(audioClient, voiceChannel, quotePath);

            return quoteText;
        }

        public async Task<string> RandomWiseQuote(IAudioClient audioClient, IVoiceChannel voiceChannel)
        {
            var quotePath = GetRandomQuotePath("WiseQuotes");
            var quoteText = GetTextOfQuote(quotePath);

            await VoiceService.SendAsync(audioClient, voiceChannel, GetRandomQuotePath("WiseQuotes"));

            return quoteText;
        }

        public async Task<string> SummonWuju(IAudioClient audioClient, IVoiceChannel voiceChannel)
        {
            await VoiceService.SendAsync(audioClient, voiceChannel, "./sound/Quotes/AllQuotes/я-умирать.ogg");

            return "Я умирать AMM";
        }

        public async Task<string> WujuLaugh(IAudioClient audioClient, IVoiceChannel voiceChannel)
        {
            await VoiceService.SendAsync(audioClient, voiceChannel, GetRandomQuotePath("Laugh"));

            return "АХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХА" +
                "АХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХА" +
                "АХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХААХАХАХАХА";
        }

        private static string GetRandomQuotePath(string quotesDirectory)
        {
            var random = new Random();
            var allFilesName = Directory.GetFiles($"./sound/Quotes/{quotesDirectory}", "*.ogg");

            return allFilesName[random.Next(allFilesName.Length)];
        }

        private static string GetTextOfQuote(string quotePath)
        {
            return quotePath;
        }
    }
}