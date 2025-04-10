using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Domain.Entities;

namespace SystemQueueDigitalisation.Application.Services
{
    public class ServiceService : IServicesService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<int> RegisterServiceAsync(string name, string Description, int ProvidedId)
        {
            var existingService = await _serviceRepository.GetByServiceAsync(name);
            if (existingService != null)
            {
                return existingService.Id;
            }

            var service = new Service
            {
                Name = name,
                Description = Description,
                ProviderId = ProvidedId
            };

            await _serviceRepository.AddAsync(service);
            return service.Id;
        }
    }
}
