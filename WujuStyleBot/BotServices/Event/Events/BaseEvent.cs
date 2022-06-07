using System;
using System.Threading.Tasks;

namespace WujuStyleBot.BotServices.Event.Events
{
    public class BaseEvent
    {
        private bool _IsFailed = false;
        
        public virtual Task<bool> ShouldRun(DateTime signalTime)
        {
            return Task.FromResult(!_IsFailed);
        }
    }
}