using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemQueueDigitalisation.Api.RequestModel;
using SystemQueueDigitalisation.Api.RequestModel.QueueRequests;
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
            try
            {
                var queueInfo = await _queueService.GenerateQueueInfoAsync(request.ClientId, request.ServiceId);

                if (queueInfo == null)
                {
                    return NotFound(new { Message = "Failed to generate queue information." });
                }

                return Ok(new
                {
                    Message = "Queue generated successfully",
                    Queue = queueInfo
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred", Error = ex.Message });
            }
        }


        [HttpPost("call-next")]
        public async Task<IActionResult> CallNextClient([FromBody] CallNextClientRequest request)
        {
            try
            {
                await _queueService.CallNextClientAsync(request.ServiceId);
                return Ok(new { Message = "Next client called." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred", Error = ex.Message });
            }
        }

        [HttpGet("status/{clientId}")]
        public async Task<ActionResult<List<QueueInfoRequest>>> GetQueueStatus(int clientId)
        {
            try
            {
                var queues = await _queueService.GetQueueStatusAsync(clientId);
                return Ok(queues);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred", Error = ex.Message });
            }
        }

        [HttpGet("count/{clientId}")]
        public async Task<IActionResult> GetCountByClient(int clientId, DateTime date)
        {
            try
            {
                var count = await _queueService.GetCountByClient(clientId, date);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred", Error = ex.Message });
            }
        }
        [HttpPost("book-appointment")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentRequest request)
        {
            try
            {
                var appointmentId = await _queueService.BookAppointmentAsync(request.ClientId, request.AppointmentDate, request.ServiceId);

                if (appointmentId <= 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        Message = "Failed to book appointment.",
                        Success = false
                    });
                }

                return Ok(new ApiResponse
                {
                    ClientId = request.ClientId,
                    ProviderId = request.ServiceId,
                    AppointmentDate = request.AppointmentDate,
                    Message = "Appointment booked successfully",
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                {
                    Message = "An error occurred",
                    Success = false
                });
            }
        }


        // POST: api/queue/generate-appointments-for-day
        [HttpPost("generate-appointments")]
        public async Task<IActionResult> GenerateAppointmentsForDay([FromBody] GenerateAppointmentsForDayRequest request)
        {
            try
            {
                await _queueService.GenerateAppointmentsForDayAsync(request.Day, request.Hour, request.appointmentTime);
                return Ok(new
                {
                    Message = "Appointments generated successfully for the day."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred", Error = ex.Message });
            }
        }

        // GET: api/queue/appointments/{clientId}
        [HttpGet("appointments/{clientId}")]
        public async Task<ActionResult<List<QueueInfoRequest>>> GetAppointmentsByClient(int clientId)
        {
            try
            {
                var appointments = await _queueService.GetAppointmentsByClientAsync(clientId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred", Error = ex.Message });
            }
        }
    }
}
