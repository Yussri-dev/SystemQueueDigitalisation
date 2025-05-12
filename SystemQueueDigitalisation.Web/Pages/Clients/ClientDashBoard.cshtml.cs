using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Clients
{
    public class ClientDashBoardModel : PageModel
    {
        private readonly QueueService _queueService;
        public ClientDashBoardModel(QueueService queueService)
        {
            _queueService = queueService;
        }

        public int ClientId { get; set; }
        public string ClientEmail { get; set; }
        public int Count { get; set; }
        public int UpcomingAppointments { get; set; }

        public async Task<IActionResult> OnGet()
        {
            // Retrieve provider information from session
            ClientId = HttpContext.Session.GetInt32("ClientId") ?? 0;
            ClientEmail = HttpContext.Session.GetString("ClientEmail");

            Count = await _queueService.GetCountByClient(ClientId,DateTime.Now);

            var allQueues = await _queueService.GetQueueStatusAsync(ClientId);

            UpcomingAppointments = allQueues
                .Where(q => q.AppointmentTime >= DateTime.Now && !q.IsServed)
                .Count();

            if (string.IsNullOrEmpty(ClientEmail))
            {
                return RedirectToPage("/Clients/ClientLogin");
            }

            return Page();
        }
    }
}
