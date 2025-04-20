using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Providers
{
    public class ProviderRegisterModel : PageModel
    {
        private readonly ProviderService _providerService;

        public ProviderRegisterModel(ProviderService providerService)
        {
            _providerService = providerService;
        }

        [BindProperty]
        public RegisterProviderRequest RegisterRequest { get; set; }

        public string Message { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Message = await _providerService.RegisterProviderAsync(RegisterRequest);
            return Page();
        }
    }
}
