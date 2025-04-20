using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SystemQueueDigitalisation.Web.Pages.Clients
{
    public class ClientDashBoardModel : PageModel
    {
        public int ClientId { get; set; }
        public string ClientEmail { get; set; }

        public IActionResult OnGet()
        {
            // Retrieve provider information from session
            ClientId = HttpContext.Session.GetInt32("ClientId") ?? 0;
            ClientEmail = HttpContext.Session.GetString("ClientEmail");

            if (
                //ProviderId == 0 
                //|| 
                string.IsNullOrEmpty(ClientEmail))
            {
                return RedirectToPage("/Clients/ClientLogin");
            }

            return Page();
        }
    }
}
