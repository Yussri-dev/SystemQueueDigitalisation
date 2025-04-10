using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface IProviderService
    {
        Task RegisterProviderAsync(string name, string email, string password, string type);
        Task<bool> AuthenticateProviderAsync(string email, string password);
    }
}
