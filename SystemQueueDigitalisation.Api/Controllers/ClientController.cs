using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemQueueDigitalisation.Api.RequestModel;
using SystemQueueDigitalisation.Application.Interfaces.Services;

namespace SystemQueueDigitalisation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterClientRequest request)
        {
            var clientId = await _clientService.RegisterClientAsync(
                    request.FirstName,
                    request.LastName,
                    request.ContactInfo,
                    request.Adress,
                    request.City,
                    request.PostCode,
                    request.AdressNumber,
                    request.BoxNumber,
                    request.BirthDate,
                    request.Email,
                    request.PasswordHash,
                    request.Age
                );
            return Ok(new { ClientId = clientId });
        }
    }
}
