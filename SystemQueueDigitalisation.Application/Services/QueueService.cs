using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Domain.Entities;

namespace SystemQueueDigitalisation.Application.Services
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _queueRepository;

        public QueueService(IQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public async Task<string> GenerateQueueNumberAsync(int clientId, int serviceId)
        {
            var queueNumber = $"Q{DateTime.UtcNow:yyyyMMddHHmmss}";
            var queue = new Queue
            {
                QueueNumber = queueNumber,
                ClientId = clientId,
                ServiceId = serviceId,
                CreatedAt = DateTime.UtcNow,
                IsServed = false
            };

            await _queueRepository.AddAsync(queue);
            return queueNumber;
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
    }
}
