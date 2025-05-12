using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemQueueDigitalisation.Api.RequestModel;
using SystemQueueDigitalisation.Api.RequestModel.ClientRequests;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Domain.Dtos;
using SystemQueueDigitalisation.Domain.Entities;
using SystemQueueDigitalisation.Web.Services;

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

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateClientRequest request)
        {
            var clientId = await _clientService.AuthenticateClientAsync(request.Email, request.Password);

            if (clientId == null)
                return Unauthorized(new { Message = "Invalid credentials." });

            return Ok(new { Message = "Authentication successful.", ClientId = clientId });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateClient([FromBody] ClientDto updatedClient)
        {
            var result = await _clientService.UpdateClientAsync(updatedClient);
            return result ? Ok() : BadRequest("Update failed.");
        }

        [HttpGet]
        public async Task<ActionResult<Client>> GetClientByEmailAsync([FromQuery] string email)
        {
            var client = await _clientService.GetClientByEmailAsync(email);
            if (client == null)
            {
                return NotFound("Client not found.");
            }
            return Ok(client);
        }

        

    }
}
