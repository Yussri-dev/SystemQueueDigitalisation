using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface IQueueService
    {
        Task<string> GenerateQueueNumberAsync(int clientId, int serviceId);
        Task CallNextClientAsync(int serviceId);
    }
}
