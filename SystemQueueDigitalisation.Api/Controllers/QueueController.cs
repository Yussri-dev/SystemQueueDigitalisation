using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemQueueDigitalisation.Api.RequestModel;
using SystemQueueDigitalisation.Application.Interfaces.Services;

namespace SystemQueueDigitalisation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateQueueNumber([FromBody] GenerateQueueRequest request)
        {
            var queueNumber = await _queueService.GenerateQueueInfoAsync(request.ClientId, request.ServiceId);
            return Ok(new { 
                QueueNumber = queueNumber,
                
            });
        }


        [HttpPost("call-next")]
        public async Task<IActionResult> CallNextClient([FromBody] CallNextClientRequest request)
        {
            await _queueService.CallNextClientAsync(request.ServiceId);
            return Ok(new { Message = "Next client called." });
        }
    }
}
