using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace WujuStyleBot.BotServices.Event.Events
{
    
    public class DeferredEvent : BaseEvent
    {
        public string Name { get; set; }
        
        public DateTime StartAt;

        public SocketInteraction DiscordInteraction;

        private bool _hasRun;
        
        public async Task Execute(DateTime signalTime, CancellationToken token)
        {
            await DiscordInteraction.FollowupAsync($"событие {Name} завершено");
            _hasRun = true;
        }
        
        public override  async Task<bool> ShouldRun(DateTime signalTime)
        {
            return await base.ShouldRun(signalTime) && !_hasRun && StartAt < signalTime;
        }
    }
}