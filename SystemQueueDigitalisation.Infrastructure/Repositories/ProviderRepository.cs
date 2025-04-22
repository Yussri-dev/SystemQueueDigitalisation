

using Microsoft.EntityFrameworkCore;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Domain.Entities;
using SystemQueueDigitalisation.Infrastructure.Data;

namespace SystemQueueDigitalisation.Infrastructure.Repositories
{
    public class ProviderRepository : RepositoryBase<Provider>, IProviderRepository
    {
        private readonly ApplicationDbContext _context;

        public ProviderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Provider> GetByEmailAsync(string email)
        {
            return await _context.Providers.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<int?> GetIdByEmailAsync(string email)
        {
            return await _context.Providers
                .Where(p => p.Email == email)
                .Select(p => (int?)p.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<Provider?> GetWithServicesAsync(int providerId)
        {
            return await _context.Providers
                .Include(p => p.Services)
                .FirstOrDefaultAsync(p => p.Id == providerId);
        }

        public async Task<List<Queue>> GetQueueByServicesAndDateAsync(List<int> serviceIds, DateTime date)
        {
            return await _context.Queues
                .Include(q => q.Client)
                .Include(q => q.Service)
                .Where(q => serviceIds.Contains(q.ServiceId) && q.CreatedAt.Date == date.Date)
                .ToListAsync();
        }

        public async Task<Queue?> GetQueueByIdAsync(int queueId)
        {
            return await _context.Queues.FirstOrDefaultAsync(q => q.Id == queueId);
        }

        public async Task UpdateQueueAsync(Queue queue)
        {
            _context.Queues.Update(queue);
            await _context.SaveChangesAsync();
        }

        // Corrected async method for GetServicesByProviderAsync
        public async Task<List<Service>> GetServicesByProviderAsync(int providerId)
        {
            return await _context.Services
                .Where(s => s.ProviderId == providerId)
                .ToListAsync();
        }
    }

}
