using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Clients
{
    public class RegisterModel : PageModel
    {
        private readonly ProviderService _providerService;

        public RegisterModel(ProviderService providerService)
        {
            _providerService = providerService;
        }

        [BindProperty]
        public RegisterProviderRequest RegisterRequest { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Message = await _providerService.RegisterProviderAsync(RegisterRequest);
            return Page();
        }
    }
}
