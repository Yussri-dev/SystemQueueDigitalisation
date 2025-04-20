using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Domain.Entities;

namespace SystemQueueDigitalisation.Application.Interfaces
{
    public interface IQueueRepository : IRepository<Queue>
    {
        Task<Queue> GetNextInQueueAsync(int serviceId);
        Task<IEnumerable<Queue>> GetQueuesByProviderIdAsync(int providerId);
        Task<Service> GetServiceWithProviderAsync(int serviceId);
        Task<Client> GetClientByIdAsync(int clientId);

    }
}
