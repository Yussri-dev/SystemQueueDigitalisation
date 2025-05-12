using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Domain.Dtos;
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

        public async Task<Client> GetByEmailAsync(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<int?> GetIdByEmailAsync(string email)
        {
            return await _context.Clients.Where(p => p.Email == email).Select(p => (int?)p.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateClientAsync(ClientDto updatedClient)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == updatedClient.Id);
            if (client == null)
                return false;

            client.FirstName = updatedClient.FirstName;
            client.LastName = updatedClient.LastName;
            client.ContactInfo = updatedClient.ContactInfo;
            client.Adress = updatedClient.Adress;
            client.City = updatedClient.City;
            client.PostCode = updatedClient.PostCode;
            client.AdressNumber = updatedClient.AdressNumber;
            client.BoxNumber = updatedClient.BoxNumber;
            client.BirthDate = updatedClient.BirthDate;
            client.Email = updatedClient.Email;
            //client.Age = updatedClient.Age;

            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
