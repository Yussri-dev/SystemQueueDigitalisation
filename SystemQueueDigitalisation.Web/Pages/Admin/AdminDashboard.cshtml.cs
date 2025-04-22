using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Admin
{
    public class AdminDashboardModel : PageModel
    {
        private readonly AdminService _adminService;

        public AdminDashboardModel(AdminService adminService)
        {
            _adminService = adminService;
        }

        public List<ProviderInfoRequest> PendingPayments { get; set; } = new List<ProviderInfoRequest>();
        public DashboardStats Stats { get; set; } = new DashboardStats();

        public async Task OnGetAsync()
        {
            PendingPayments = await _adminService.GetPendingPayments();

            // Pour les statistiques, vous devrez peut-être créer d'autres méthodes dans votre service
            // Ceci est un exemple simple
            Stats.TotalProviders = 0; // À remplacer par votre logique
            Stats.ConfirmedPayments = 0; // À remplacer par votre logique
            Stats.PendingPayments = PendingPayments.Count;
        }

        public async Task<IActionResult> OnPostConfirmPaymentAsync(int providerId)
        {
            bool result = await _adminService.ConfirmManualPayment(providerId);

            if (result)
            {
                TempData["SuccessMessage"] = "Paiement confirmé avec succès.";
            }
            else
            {
                TempData["ErrorMessage"] = "Erreur lors de la confirmation du paiement.";
            }

            return RedirectToPage();
        }
    }

    public class DashboardStats
    {
        public int TotalProviders { get; set; }
        public int ConfirmedPayments { get; set; }
        public int PendingPayments { get; set; }
    }
}