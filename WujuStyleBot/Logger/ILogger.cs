using System.Threading.Tasks;
using Discord;

namespace WujuStyleBot.Logger
{
    public interface ILogger
    {
        public Task Log(LogMessage message);
    }
}