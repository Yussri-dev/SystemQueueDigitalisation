using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Domain.Dtos;
using SystemQueueDigitalisation.Domain.Entities;
using SystemQueueDigitalisation.Web.Requests.ProviderRequests;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface IProviderService
    {
        Task RegisterProviderAsync(string name, string email, string password, string type);
        //Task<bool> AuthenticateProviderAsync(string email, string password);
        Task<Provider?> AuthenticateProviderAsync(string email, string password);

        Task<List<QueueInfo>> GetTodayQueueAsync(int providerId);
        Task<List<QueueInfo>> GetQueuesByDateAsync(int providerId, DateTime date);
        Task MarkAsServedAsync(int queueId);
        Task<List<ServiceDto>> GetServicesByProvider(int providerId);
        Task<List<ProviderRequest>> GetAllProviders();

    }
}
