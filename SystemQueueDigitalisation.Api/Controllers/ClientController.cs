using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    public class RegisterClientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactInfo { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string AdressNumber { get; set; }
        public int BoxNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }


    }
}
