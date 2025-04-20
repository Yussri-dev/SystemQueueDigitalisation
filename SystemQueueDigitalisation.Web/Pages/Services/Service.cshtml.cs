using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Services
{
    public class ServiceModel : PageModel
    {
        private readonly ServiceService _serviceService;

        public ServiceModel(ServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [BindProperty]
        public RegisterServiceRequest RegisterServiceRequest { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet()
        {
            var providerId = HttpContext.Session.GetInt32("ProviderId");
            if (providerId == null || providerId == 0)
            {
                return RedirectToPage("/Providers/ProviderLogin");
            }

            RegisterServiceRequest = new RegisterServiceRequest
            {
                ProviderId = providerId.Value
            };

            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var providerId = HttpContext.Session.GetInt32("ProviderId");
            if (providerId == null || providerId == 0)
            {
                Message = "You must be logged in as a provider to register a service.";
                return RedirectToPage("/Providers/ProviderLogin");
            }

            RegisterServiceRequest.ProviderId = providerId.Value;

            Message = await _serviceService.RegisterServiceAsync(RegisterServiceRequest);
            return Page();
        }

    }
}
