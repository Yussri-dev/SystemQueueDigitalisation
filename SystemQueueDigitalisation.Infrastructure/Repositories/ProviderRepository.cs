

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
    }
}
