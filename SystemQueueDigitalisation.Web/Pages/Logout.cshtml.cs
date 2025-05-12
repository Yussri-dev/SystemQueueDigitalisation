using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ISessionService _sessionService;

        public LogoutModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            _sessionService.ClearSession();

            return RedirectToPage("/Index"); 
        }
    }
}
