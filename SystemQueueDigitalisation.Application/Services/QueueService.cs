using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Domain.Entities;
using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Application.Services
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IServiceRepository _serviceRepository;

        public QueueService(
        IQueueRepository queueRepository,
        IClientRepository clientRepository,
        IServiceRepository serviceRepository)
        {
            _queueRepository = queueRepository;
            _clientRepository = clientRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<QueueInfoRequest> GenerateQueueNumberAsync(int clientId, int serviceId, string email)
        {
            // Fetch client details
            var client = await _clientRepository.GetByEmailAsync(email);
            if (client == null)
                throw new InvalidOperationException("Client not found.");

            // Fetch service details
            var service = await _serviceRepository.GetByIdAsync(serviceId);
            if (service == null)
                throw new InvalidOperationException("Service not found.");

            // Generate a unique queue number
            var queueNumber = $"Q{DateTime.UtcNow:yyyyMMddHHmmss}";

            // Save the queue entry to the database
            var queue = new Queue
            {
                QueueNumber = queueNumber,
                ClientId = clientId,
                ServiceId = serviceId,
                CreatedAt = DateTime.UtcNow
            };
            await _queueRepository.AddAsync(queue);

            // Return the response with additional details
            return new QueueInfoRequest
            {
                QueueNumber = queueNumber,
                ClientEmail = client.Email,
                ServiceName = service.Name,
                ProviderName = service.Provider.Name // Assuming the service has a navigation property to the provider
            };
        }

        public async Task CallNextClientAsync(int serviceId)
        {
            var nextInQueue = await _queueRepository.GetNextInQueueAsync(serviceId);
            if (nextInQueue == null)
                throw new Exception("No clients in the queue.");

            nextInQueue.CalledAt = DateTime.UtcNow;
            nextInQueue.IsServed = true;

            await _queueRepository.UpdateAsync(nextInQueue);
        }

        public async Task<QueueInfoRequest> GenerateQueueInfoAsync(int clientId, int serviceId)
        {
            var queueNumber = $"Q{DateTime.UtcNow:yyyyMMddHHmmss}";

            var service = await _queueRepository.GetServiceWithProviderAsync(serviceId);
            var client = await _queueRepository.GetClientByIdAsync(clientId);

            var queue = new Queue
            {
                QueueNumber = queueNumber,
                ClientId = clientId,
                ServiceId = serviceId,
                CreatedAt = DateTime.UtcNow,
                IsServed = false
            };

            await _queueRepository.AddAsync(queue);

            return new QueueInfoRequest
            {
                QueueNumber = queueNumber,
                ClientEmail = client?.Email,
                ServiceName = service?.Name,
                ProviderName = service?.Provider?.Name,
                ProviderType = service?.Provider?.Type
            };
        }

        public Task<string> GenerateQueueNumberAsync(int clientId, int serviceId)
        {
            throw new NotImplementedException();
        }
    }
}
