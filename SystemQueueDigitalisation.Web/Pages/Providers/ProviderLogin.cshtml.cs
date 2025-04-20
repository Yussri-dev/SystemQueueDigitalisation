using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Providers
{
    public class ProviderLoginModel : PageModel
    {
        private readonly ProviderService _providerService;

        public ProviderLoginModel(ProviderService providerService)
        {
            _providerService = providerService;
        }

        [BindProperty]
        public AuthenticateProviderRequest LoginRequest { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var authResponse = await _providerService.AuthenticateProviderAsync(LoginRequest);

            if (authResponse.ProviderId == 0)
            {
                Message = authResponse.Message;
                return Page();
            }

            HttpContext.Session.SetInt32("ProviderId", authResponse.ProviderId);
            HttpContext.Session.SetString("ProviderEmail", LoginRequest.Email);

            Message = authResponse.Message;
            return RedirectToPage("/Providers/ProviderDashBoard");
        }

    }
}
