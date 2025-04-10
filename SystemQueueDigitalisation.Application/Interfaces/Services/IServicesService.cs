using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface IServicesService
    {
        Task<int> RegisterServiceAsync(string name, string Description, int ProvidedId);
    }
}
