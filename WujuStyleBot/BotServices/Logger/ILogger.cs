using System.Threading.Tasks;
using Discord;

namespace WujuStyleBot.BotServices.Logger
{
    public interface ILogger
    {
        public Task Log(LogMessage message);
    }
}