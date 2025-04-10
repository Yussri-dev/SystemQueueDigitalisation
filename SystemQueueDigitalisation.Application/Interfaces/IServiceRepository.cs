using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Domain.Entities;

namespace SystemQueueDigitalisation.Application.Interfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<Service>> GetByProviderIdAsync(int providerId);
        Task<Service> GetByServiceAsync(string contactInfo);

    }
}
