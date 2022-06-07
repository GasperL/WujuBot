using System;
using System.Threading.Tasks;
using Discord;

namespace WujuStyleBot.Logger
{
    public abstract class Logger : ILogger
    {
        protected string _guid;

        protected Logger()
        {
            _guid = Guid.NewGuid().ToString();
        }

        public abstract Task Log(LogMessage message);
    }
}