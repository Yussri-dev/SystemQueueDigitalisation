using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(string userId, string userName, IList<string> roles);
    }
}
