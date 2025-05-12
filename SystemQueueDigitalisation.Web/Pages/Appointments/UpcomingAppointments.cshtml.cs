using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Appointments
{
    public class UpcomingAppointmentsModel : PageModel
    {
        private readonly QueueService _queueService;
        private readonly ClientService _clientService;

        public UpcomingAppointmentsModel(QueueService queueService, ClientService clientService)
        {
            _queueService = queueService;
            _clientService = clientService;
        }

        public List<QueueInfoRequest> UpcomingAppointments { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var email = HttpContext.Session.GetString("ClientEmail");

            if (string.IsNullOrEmpty(email))
                return RedirectToPage("/Login");

            var client = await _clientService.GetClientByEmailAsync(email);

            if (client == null || client.Id == 0)
                return RedirectToPage("/Login");

            var allQueues = await _queueService.GetQueueStatusAsync(client.Id);

            // Filter: future Appointments dates
            UpcomingAppointments = allQueues
                .Where(q => q.AppointmentTime >= DateTime.Now && !q.IsServed)
                .OrderBy(q => q.AppointmentTime)
                .ToList();

            return Page();
        }
    }
}
