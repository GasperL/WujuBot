using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WujuStyleBot.BotServices.Event.Events;

namespace WujuStyleBot.BotServices.Event
{
    public interface IEventService
    {
        Task RegisterEvent(DeferredEvent @event);

        void Start();

        void Stop();

        List<DeferredEvent> GetEventList();
    }
}