using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Domain.Entities;
using SystemQueueDigitalisation.Infrastructure.Data;

namespace SystemQueueDigitalisation.Infrastructure.Repositories
{
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetByProviderIdAsync(int providerId)
        {
            return await _context.Services.Where(s => s.ProviderId == providerId).ToListAsync();
        }

        public async Task<Service> GetByServiceAsync(string serviceName)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Name == serviceName);
        }
    }
}
