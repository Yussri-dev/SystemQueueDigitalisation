using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Dtos;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<AuthResponseDto> AuthenticateAsync(LoginDto request);
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

    }
}
