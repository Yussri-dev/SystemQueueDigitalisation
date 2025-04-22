using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemQueueDigitalisation.Api.RequestModel;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var provider = await _providerService.AuthenticateProviderAsync(request.Email, request.Password);

            if (provider == null)
                return Unauthorized(new { Message = "Invalid credentials." });

            if (!provider.IsPaymentConfirmed)
            {
                return Unauthorized(new { Message = "Payment is required to use the System" });
            }
            return Ok(new { Message = "Authentication successful.", ProviderId = provider.Id });
        }

        [HttpGet("{providerId}/queues/today")]
        public async Task<ActionResult<List<QueueInfoRequest>>> GetTodayQueues(int providerId)
        {
            var queues = await _providerService.GetTodayQueueAsync(providerId);
            Console.WriteLine($"API returning {queues.Count} queues for provider {providerId}");
            return Ok(queues);
        }

        [HttpPut("queue/{queueId}/serve")]
        public async Task<IActionResult> MarkQueueAsServed(int queueId)
        {
            await _providerService.MarkAsServedAsync(queueId);
            return Ok(new { Message = "Queue marked as served." });
        }

        [HttpGet("{providerId}/queues")]
        public async Task<ActionResult<List<QueueInfoRequest>>> GetQueuesByDate(int providerId, DateTime date)
        {
            var queues = await _providerService.GetQueuesByDateAsync(providerId, date);
            return Ok(queues);
        }

        [HttpGet("byprovider/{providerId}")]
        public async Task<IActionResult> GetServicesByProvider(int providerId)
        {
            var services = await _providerService.GetServicesByProvider(providerId);
            return Ok(services);
        }


    }
}
