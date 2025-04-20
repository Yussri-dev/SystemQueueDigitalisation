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
    public class QueueRepository : RepositoryBase<Queue>, IQueueRepository
    {
        private readonly ApplicationDbContext _context;

        public QueueRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Queue> GetNextInQueueAsync(int serviceId)
        {
            return await _context.Queues
                .FirstOrDefaultAsync(q => q.ServiceId == serviceId && !q.IsServed);
        }

        public async Task<IEnumerable<Queue>> GetQueuesByProviderIdAsync(int providerId)
        {
            return await _context.Queues
                .Where(q => q.Service.ProviderId == providerId)
                .ToListAsync();
        }

        public async Task<Service> GetServiceWithProviderAsync(int serviceId)
        {
            return await _context.Services
                .Include(s => s.Provider)
                .FirstOrDefaultAsync(s => s.Id == serviceId);
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == clientId);
        }
    }
}
