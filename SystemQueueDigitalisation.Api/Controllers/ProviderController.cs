using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemQueueDigitalisation.Api.RequestModel;
using SystemQueueDigitalisation.Application.Interfaces.Services;

namespace SystemQueueDigitalisation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterProviderRequest request)
        {
            await _providerService.RegisterProviderAsync(request.Name, request.Email, request.Password, request.Type);
            return Ok(new { Message = "Provider registered successfully." });
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateProviderRequest request)
        {
            var isAuthenticated = await _providerService.AuthenticateProviderAsync(request.Email, request.Password);

            if (!isAuthenticated)
                return Unauthorized(new { Message = "Invalid credentials." });

            return Ok(new { Message = "Authentication successful." });
        }
    }
}
