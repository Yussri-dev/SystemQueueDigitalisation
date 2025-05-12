using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemQueueDigitalisation.Web.Dtos;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Admin
{
    public class AdminLoginModel : PageModel
    {
        private readonly AuthService _authService;
        private readonly ISessionService _sessionService;

        public AdminLoginModel(AuthService authService, ISessionService sessionService)
        {
            _authService = authService;
            _sessionService = sessionService;
        }

        [BindProperty]
        public LoginRequest Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _authService.LoginAsync(Input);

            if (result.IsSuccess)
            {
                // Set roles in session
                _sessionService.SetToken(result.Token);
                _sessionService.SetUserRole(result.Roles);

                var roles = _sessionService.GetUserRoles();
                if (roles.Contains("Admin") || roles.Contains("Owner"))
                {
                    return RedirectToPage("/Admin/AdminDashboard");
                }

                // Non autorisé - déconnexion
                _authService.Logout();
                ModelState.AddModelError(string.Empty, "You do not have permissions");
                return Page();
            }

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return Page();
        }
    }
}
