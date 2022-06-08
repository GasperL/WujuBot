using System.Diagnostics;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;

namespace WujuStyleBot.BotServices.WujuVoice
{
    public static class VoiceService
    {
        public static async Task SendAsync(IAudioClient client, IVoiceChannel channel, string path)
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

        private static Process CreateStream(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true
            });
        }
    }
}

