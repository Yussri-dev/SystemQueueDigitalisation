using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Providers
{
    public class ProviderDashBoardModel : PageModel
    {
        private readonly ProviderService _providerService;

        private readonly IConverter _pdfConverter;

        public ProviderDashBoardModel(ProviderService providerService, IConverter pdfConverter)
        {
            _providerService = providerService;
            _pdfConverter = pdfConverter;
        }

        
        public int ProviderId { get; set; }
        public string ProviderEmail { get; set; }
        public List<QueueInfo> TodayQueues { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ProviderId = HttpContext.Session.GetInt32("ProviderId") ?? 0;
            ProviderEmail = HttpContext.Session.GetString("ProviderEmail");

            if (string.IsNullOrEmpty(ProviderEmail))
                return RedirectToPage("/Providers/ProviderLogin");

            if (FilterDate.HasValue && FilterDate.Value.Date != DateTime.Today)
            {
                TodayQueues = await _providerService.GetQueuesByDateAsync(ProviderId, FilterDate.Value);
            }
            else
            {
                TodayQueues = await _providerService.GetTodayQueuesAsync(ProviderId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostServeQueueAsync(int queueId)
        {
            await _providerService.MarkAsServedAsync(queueId);
            return RedirectToPage();
        }

        //csv
        public async Task<IActionResult> OnGetExportCsvAsync()
        {
            ProviderId = HttpContext.Session.GetInt32("ProviderId") ?? 0;
            var queues = FilterDate.HasValue
                ? await _providerService.GetQueuesByDateAsync(ProviderId, FilterDate.Value)
                : await _providerService.GetTodayQueuesAsync(ProviderId);

            var csv = new StringBuilder();
            csv.AppendLine("QueueNumber,ClientEmail,ServiceName,CreatedAt,IsServed");

            foreach (var q in queues)
            {
                csv.AppendLine($"{q.QueueNumber},{q.ClientEmail},{q.ServiceName},{q.CreatedAt:yyyy-MM-dd HH:mm},{q.IsServed}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", $"queues_{DateTime.Now:yyyyMMddHHmm}.csv");
        }

        //pdf



    }

}
