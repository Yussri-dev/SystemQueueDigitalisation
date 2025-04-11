using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ProviderService _providerService;

        public LoginModel(ProviderService providerService)
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
                return Page();

            Message = await _providerService.AuthenticateProviderAsync(LoginRequest);
            return Page();
        }
    }
}
