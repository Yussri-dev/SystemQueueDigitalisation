using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Domain.Dtos;
using SystemQueueDigitalisation.Domain.Entities;

namespace SystemQueueDigitalisation.Application.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetByContactInfoAsync(string contactInfo);
        Task<Client> GetByEmailAsync(string email);
        Task<int?> GetIdByEmailAsync(string email);
        Task<bool> UpdateClientAsync(ClientDto updatedClient);
    }
}
