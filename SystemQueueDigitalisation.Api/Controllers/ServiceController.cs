﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Application.Services;

namespace SystemQueueDigitalisation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServicesService _serviceservice;

        public ServiceController(IServicesService serviceService)
        {
            _serviceservice = serviceService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterServiceRequest request)
        {
            var serviceId = await _serviceservice.RegisterServiceAsync(request.Name, request.Description, request.ProviderId);
            return Ok(new { ServiceId = serviceId });
        }

        public class RegisterServiceRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int ProviderId { get; set; }
        }
    }
}
