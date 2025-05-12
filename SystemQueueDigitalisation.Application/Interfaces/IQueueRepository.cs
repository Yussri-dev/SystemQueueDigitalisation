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
        Task<List<Queue>> GetQueuesByDateAsync(DateTime day);
        Task<List<Queue>> GetAppointmentsByClientIdAsync(int clientId);
        Task<Service> GetServiceWithProviderAsync(int serviceId);
        Task<Client> GetClientByIdAsync(int clientId);
        Task<IEnumerable<Queue>> GetQueuesByClientIdAsync(int clientId);
        Task<int> GetCountByClient(int clientId, DateTime date);
        Task<int> GetQueuesByClientIdWithServiceIdByDateAsync(int clientId, int serviceId, DateTime date);

    }
}
