using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Requests.Appointments;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Queue
{
    public class BookAppointmentModel : PageModel
    {
        private readonly QueueService _queueService;

        public BookAppointmentModel(QueueService queueService)
        {
            _queueService = queueService;
        }

        // This will bind the data to the form
        [BindProperty]
        public BookAppointmentRequest BookAppointmentRequest { get; set; }

        public string Message { get; set; }

        // OnPostAsync is called when the form is submitted
        public async Task OnPostAsync()
        {
            // Get ClientId from session
            var clientId = HttpContext.Session.GetInt32("ClientId");

            if (clientId == null)
            {
                Message = "Client ID is required.";
                return;
            }

            try
            {
                BookAppointmentRequest.ClientId = clientId.Value;

                var response = await _queueService.BookAppointmentAsync(BookAppointmentRequest);

                if (response.Success)
                {
                    Message = $"Appointment booked successfully. Client ID: {response.ClientId}";
                }
                else
                {
                    Message = "Failed to book appointment.";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error: {ex.Message}";
            }
        }
    }
}
