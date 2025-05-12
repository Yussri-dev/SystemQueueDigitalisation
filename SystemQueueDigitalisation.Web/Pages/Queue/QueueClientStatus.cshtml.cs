using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Queue
{
    public class QueueClientStatusModel : PageModel
    {
        private readonly ClientService _clientService;
        private readonly QueueService _queueService;

        public QueueClientStatusModel(ClientService clientService, QueueService queueService)
        {
            _clientService = clientService;
            _queueService = queueService;
        }

        [BindProperty(SupportsGet = true)]
        public string SelectedFilter { get; set; }

        public List<QueueInfoRequest> Queues { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var email = HttpContext.Session.GetString("ClientEmail");

            if (string.IsNullOrEmpty(email))
                return RedirectToPage("/Login");

            var client = await _clientService.GetClientByEmailAsync(email);

            if (client == null || client.Id == 0)
                return RedirectToPage("/Login");

            Queues = await _queueService.GetQueueStatusAsync(client.Id);
            return Page();
        }
    }
}
