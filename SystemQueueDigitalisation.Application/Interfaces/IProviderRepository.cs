using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Domain.Entities;

namespace SystemQueueDigitalisation.Application.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider?> GetByEmailAsync(string email);
        Task<int?> GetIdByEmailAsync(string email);
        Task<Provider?> GetWithServicesAsync(int providerId);
        Task<List<Queue>> GetQueueByServicesAndDateAsync(List<int> serviceIds, DateTime date);
        Task<Queue?> GetQueueByIdAsync(int queueId);
        Task UpdateQueueAsync(Queue queue);
        Task<List<Service>> GetServicesByProviderAsync(int providerId);
        Task<List<Provider>> GetAllProviders();
    }

}
