using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Requests.ClientRequests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Clients
{
    public class ClientProfileModel : PageModel
    {
        private readonly ClientService _clientService;


        public ClientProfileModel(ClientService clientService)
        {
            _clientService = clientService;
        }

        [BindProperty]
        public ClientProfileRequest ClientProfileRequest { get; set; }

        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            var email = HttpContext.Session.GetString("ClientEmail");

            if (!string.IsNullOrEmpty(email))
            {
                var client = await _clientService.GetClientByEmailAsync(email);

                if (client != null)
                {
                    ClientProfileRequest = client;
                }
            }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updateResult = await _clientService.UpdateClientAsync(ClientProfileRequest);

            if (!updateResult)
            {
                Message = "Failed to update profile. Please try again.";
                return Page();
            }

            Message = "Profile updated successfully!";

            return RedirectToPage("/Clients/ClientDashBoard");
        }

    }
}
