using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Web.Hubs;

namespace SystemQueueDigitalisation.Infrastructure.Repositories
{
    class NotificationRepository : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationRepository(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task NotifyNewQueueAsync(object queueData)
        {
            throw new NotImplementedException();
        }

        public Task NotifyQueueUpdatedAsync(object queueData)
        {
            throw new NotImplementedException();
        }

        public async Task NotifyUserAsync(string userId, string message)
        {
            await _hubContext.Clients.User(userId).SendAsync("Receive message", message);
        }
    }
}
