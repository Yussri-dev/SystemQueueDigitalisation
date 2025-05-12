using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests.Appointments;
using SystemQueueDigitalisation.Web.Requests.ClientRequests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Appointments
{
    public class AppointmentGeneratorModel : PageModel
    {
        private readonly QueueService _queueService;

        public AppointmentGeneratorModel(QueueService queueService)
        {
            _queueService = queueService;
        }

        [BindProperty]
        public GenerateAppointmentsForDayRequest AppointmentRequest { get; set; }

        public void OnGetAsync()
        {
            //var email = HttpContext.Session.GetString("ClientEmail");

            //if (!string.IsNullOrEmpty(email))
            //{

            //}
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var appointmentResponse = await _queueService.GenerateAppointmentsForDayAsync(AppointmentRequest);

            return RedirectToPage("/Appointments/AppointmentGenerator");
        }
    }
}
