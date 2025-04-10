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
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Client> GetByContactInfoAsync(string contactInfo)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.ContactInfo == contactInfo);
        }
    }
}
