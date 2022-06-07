using System;
using System.Threading.Tasks;
using Discord;

namespace WujuStyleBot.Logger
{
    public class ConsoleLogger : Logger
    {
        public override async Task Log(LogMessage message)
        {
            await Task.Run(() => LogToConsoleAsync(message));
        }

        private Task LogToConsoleAsync(LogMessage message)
        {
            Console.WriteLine($"guid:{_guid} : " + message);

            return Task.CompletedTask;
        }
    }
}