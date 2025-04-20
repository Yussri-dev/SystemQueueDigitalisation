using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface IQueueService
    {
        Task<string> GenerateQueueNumberAsync(int clientId, int serviceId);

        Task<QueueInfoRequest> GenerateQueueInfoAsync(int clientId, int serviceId);
        Task CallNextClientAsync(int serviceId);

    }
}
