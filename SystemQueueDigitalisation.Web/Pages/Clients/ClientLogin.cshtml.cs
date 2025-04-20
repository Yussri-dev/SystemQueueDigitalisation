using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Clients
{
    public class ClientLoginModel : PageModel
    {
        private readonly ClientService _clientService;

        public ClientLoginModel(ClientService clientService)
        {
            _clientService = clientService;
        }

        [BindProperty]
        public AuthenticateClientRequest LoginRequest { get; set; }

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

            var authResponse = await _clientService.AuthenticateClientAsync(LoginRequest);

            if (authResponse.ClientId == 0)
            {
                Message = authResponse.Message;
                return Page();
            }

            HttpContext.Session.SetInt32("ClientId", authResponse.ClientId);
            HttpContext.Session.SetString("ClientEmail", LoginRequest.Email);

            Message = authResponse.Message;
            return RedirectToPage("/Clients/ClientDashBoard");
        }


        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    var clientId = await _clientService.AuthenticateClientAsync(LoginRequest.Email, LoginRequest.Password);
        //    if (clientId == null)
        //    {
        //        Message = "Invalid email or password.";
        //        return Page();
        //    }

        //    HttpContext.Session.SetInt32("ClientId", clientId.Value);
        //    HttpContext.Session.SetString("ClientEmail", LoginRequest.Email);

        //    Message = "Login successful!";
        //    return RedirectToPage("/Clients/ClientDashBoard");
        //}


        /*
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = await _clientService.AuthenticateClientAsync(LoginRequest);
            if (client == null)
            {
                Message = "Invalid email or password.";
                return Page();
            }

            // Store client information in session
            HttpContext.Session.SetInt32("ClientId", LoginRequest.Id);
            HttpContext.Session.SetString("ClientEmail", LoginRequest.Email);

            Message = "Login successful!";
            return RedirectToPage("/Clients/ClientDashBoard");
            */
    }
}
