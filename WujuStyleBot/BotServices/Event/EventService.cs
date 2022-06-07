using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using WujuStyleBot.BotServices.Event.Events;
using Timer = System.Timers.Timer;

namespace WujuStyleBot.BotServices.Event
{
    public class EventService : IEventService
    {
        private readonly List<DeferredEvent> _events = new List<DeferredEvent>();
        private readonly Timer _timer;
        private CancellationTokenSource _tokenSource;

        public EventService()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        public Task RegisterEvent(DeferredEvent @event)
        {
            _events.Add(@event);
            return Task.CompletedTask;
        }
        
        public void Start()
        {
            _tokenSource = new CancellationTokenSource();
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public List<DeferredEvent> GetEventList()
        {
            return _events;
        }

        public void CancelEvent()
        {
            _tokenSource.Cancel();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs @event)
        {
            OnTimedEventAsync(@event).GetAwaiter().GetResult();
        }

        private async Task OnTimedEventAsync(ElapsedEventArgs @event)
        {
            await ExecuteDeferredEvents(@event);
        }
        
        private async Task ExecuteDeferredEvents(ElapsedEventArgs @event)
        {
            await ExecuteEvents(_events, @event.SignalTime);
        }

        private async Task ExecuteEvents(IEnumerable<DeferredEvent> @events, DateTime startAt)
        {
            foreach (var item in events)
            {
                if (await item.ShouldRun(startAt))
                {
                   await ExecuteEvent(item, startAt);
                }
            }
        }
        
        private async Task ExecuteEvent(DeferredEvent @event, DateTime signalTime)
        {
            try
            {
                 await @event.Execute(signalTime, _tokenSource.Token);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"Operation canceled. {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}