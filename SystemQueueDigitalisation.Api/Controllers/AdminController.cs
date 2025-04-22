using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemQueueDigitalisation.Api.RequestModel;
using SystemQueueDigitalisation.Application.Interfaces.Services;

namespace SystemQueueDigitalisation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("pending-payments")]
        public async Task<ActionResult<List<ProviderInfoRequest>>> GetPendingPayments()
        {
            var admins = await _adminService.GetPendingPaymentsAsync();
            return Ok(admins);
        }

        [HttpPost("{providerId}/confirm-payment")]
        public async Task<IActionResult> ConfirmManualPayment(int providerId)
        {
            await _adminService.ConfirmPaymentManuallyAsync(providerId);
            return Ok(new { Message = "Payment manually confirmed." });
        }

    }
}
