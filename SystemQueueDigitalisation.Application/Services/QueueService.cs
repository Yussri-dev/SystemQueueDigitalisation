using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Domain.Entities;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Domain.Dtos;

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

        public async Task<int> BookAppointmentAsync(int clientId, DateTime appointmentDate, int serviceId)
        {
            var queueNumber = $"A{appointmentDate:yyyyMMddHHmmss}";

            var queue = new Queue
            {
                QueueNumber = queueNumber,
                ClientId = clientId,
                ServiceId = serviceId,
                CreatedAt = DateTime.Now,
                AppointmentTime = appointmentDate,
                IsServed = false
            };

            await _queueRepository.AddAsync(queue);

            return queue.Id;
        }


        public async Task GenerateAppointmentsForDayAsync(DateTime day, int startHour, int appointmentDuration)
        {
            var queues = await _queueRepository.GetQueuesByDateAsync(day);

            if (queues == null || !queues.Any())
                return;

            var startTime = day.Date.AddHours(startHour);

            for (int i = 0; i < queues.Count; i++)
            {
                var appointmentTime = startTime.AddMinutes(i * appointmentDuration);
                queues[i].AppointmentTime = appointmentTime;
            }

            await _queueRepository.UpdateRangeAsync(queues);
        }


        public async Task<List<QueueInfoRequest>> GetAppointmentsByClientAsync(int clientId)
        {
            var queues = await _queueRepository.GetAppointmentsByClientIdAsync(clientId);

            return queues.Select(q => new QueueInfoRequest
            {
                QueueNumber = q.QueueNumber,
                CreatedAt = q.CreatedAt,
                IsServed = q.IsServed,
                AppointmentDate = q.AppointmentDate,
                AppointmentTime = q.AppointmentTime,
                ClientEmail = q.Client?.Email ?? "Unknown",
                ServiceName = q.Service?.Name ?? "Unknown",
                ProviderName = q.Service?.Provider?.Name ?? "Unknown"
            }).ToList();
        }

        public async Task<QueueInfoRequest> GenerateQueueNumberAsync(int clientId, int serviceId, string email)
        {
            // Fetch client details
            var client = await _clientRepository.GetByEmailAsync(email);
            if (client == null)
                throw new InvalidOperationException("Client not found.");

            var service = await _serviceRepository.GetByIdAsync(serviceId);
            if (service == null)
                throw new InvalidOperationException("Service not found.");

            var queueNumber = $"Q{DateTime.UtcNow:yyyyMMddHHmmss}";

            var queue = new Queue
            {
                QueueNumber = queueNumber,
                ClientId = clientId,
                ServiceId = serviceId,
                CreatedAt = DateTime.UtcNow
            };
            await _queueRepository.AddAsync(queue);

            return new QueueInfoRequest
            {
                QueueNumber = queueNumber,
                ClientEmail = client.Email,
                ServiceName = service.Name,
                ProviderName = service.Provider.Name
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
            var date = DateTime.Now;
            var queueNumber = $"Q{date:yyyyMMddHHmmss}";

            var service = await _queueRepository.GetServiceWithProviderAsync(serviceId);
            var client = await _queueRepository.GetClientByIdAsync(clientId);

            var queueCount = await _queueRepository.GetQueuesByClientIdWithServiceIdByDateAsync(clientId, serviceId, date);

            if (queueCount != 0)
            {
                return null;
            }

            var queue = new Queue
            {
                QueueNumber = queueNumber,
                ClientId = clientId,
                ServiceId = serviceId,
                CreatedAt = date,
                IsServed = false
            };

            await _queueRepository.AddAsync(queue);

            return new QueueInfoRequest
            {
                QueueNumber = queueNumber,
                ClientEmail = client.Email,
                ServiceName = service.Name,
                ProviderName = service.Provider.Name,
                ProviderType = service.Provider.Type,
                CreatedAt = date

            };
        }


        public async Task<int> GetCountByClient(int clientId, DateTime date)
        {
            var count = await _queueRepository.GetCountByClient(clientId, date);
            return count;
        }
        public async Task<List<QueueInfoRequest>> GetQueueStatusAsync(int clientId)
        {
            var queues = await _queueRepository.GetQueuesByClientIdAsync(clientId);

            return queues.Select(s => new QueueInfoRequest
            {
                QueueNumber = s.QueueNumber,
                IsServed = s.IsServed,
                CreatedAt = s.CreatedAt,
                ProviderName = s.Service.Provider.Name,
                ServiceName = s.Service.Name,
                AppointmentTime = s.AppointmentTime,
                ClientEmail = s.Client.Email
            }).ToList();
        }


    }
}
