using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task NotifyQueueUpdatedAsync(object queueData);
        Task NotifyNewQueueAsync(object queueData);
    }
}
