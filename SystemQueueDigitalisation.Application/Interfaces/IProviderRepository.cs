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
        Task<Provider> GetByEmailAsync(string email);
    }
}
