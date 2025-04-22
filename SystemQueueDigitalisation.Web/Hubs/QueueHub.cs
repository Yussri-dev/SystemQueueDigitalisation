using Microsoft.AspNetCore.SignalR;
using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Web.Hubs
{
    public class QueueHub : Hub
    {
        public async Task NotifyNewQueue(QueueInfo queueInfo)
        {
            await Clients.All.SendAsync("NewQueue", queueInfo);
        }

        // Method to notify all clients when a queue status is updated
        public async Task NotifyQueueUpdated(QueueInfo queueInfo)
        {
            await Clients.All.SendAsync("QueueUpdated", queueInfo);
        }
    }
}
