using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces.Services;


namespace SystemQueueDigitalisation.Infrastructure.Repositories
{
    class SignalRNotificationRepository 
    {
        //private readonly IHubContext<QueueHub> _hubContext;

        //public SignalRNotificationRepository(IHubContext<QueueHub> hubContext)
        //{
        //    _hubContext = hubContext;
        //}

        //public async Task NotifyQueueUpdatedAsync(object queueData)
        //{
        //    await _hubContext.Clients.All.SendAsync("QueueUpdated", queueData);
        //}

        //public async Task NotifyNewQueueAsync(object queueData)
        //{
        //    await _hubContext.Clients.All.SendAsync("NewQueue", queueData);
        //}
    }
}
