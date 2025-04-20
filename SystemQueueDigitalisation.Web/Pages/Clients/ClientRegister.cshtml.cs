using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Clients
{
    public class ClientRegisterModel : PageModel
    {
        private readonly ClientService _clientService;

        public ClientRegisterModel(ClientService clientService)
        {
            _clientService = clientService;
        }

        [BindProperty]
        public RegisterClientRequest RegisterRequest { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Message = await _clientService.RegisterClientAsync(RegisterRequest);
            return Page();
        }
    }
}
