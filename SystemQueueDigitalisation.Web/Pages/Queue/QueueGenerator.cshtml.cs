using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using QRCoder;
using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Services;

namespace SystemQueueDigitalisation.Web.Pages.Queue
{
    public class QueueGeneratorModel : PageModel
    {
        private readonly QueueService _queueService;

        public QueueGeneratorModel(QueueService queueService)
        {
            _queueService = queueService;
        }

        [BindProperty]
        public GenerateQueueNumberRequest GenerateQueueRequest { get; set; }

        [BindProperty]
        public CallNextClientRequest CallNextClientRequest { get; set; }

        public QueueInfoRequest QueueInfo { get; set; }
        public string QrCodeBase64 { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            var clientId = HttpContext.Session.GetInt32("ClientId");
            var clientEmail = HttpContext.Session.GetString("ClientEmail");

            if (clientId == null || clientId == 0 || clientEmail == null)
            {
                return RedirectToPage("/Clients/ClientLogin");
            }

            GenerateQueueRequest = new GenerateQueueNumberRequest
            {
                ClientId = clientId.Value
            };

            return Page();
        }

        public async Task<IActionResult> OnPostGenerateQueueAsync()
        {
            var clientId = HttpContext.Session.GetInt32("ClientId");
            var clientEmail = HttpContext.Session.GetString("ClientEmail");

            if (clientId == null || clientId == 0 || string.IsNullOrWhiteSpace(clientEmail))
            {
                return RedirectToPage("/Clients/ClientLogin");
            }

            GenerateQueueRequest.ClientId = clientId.Value;

            QueueInfo = await _queueService.GenerateQueueInfo(GenerateQueueRequest);

            var qrText = $"Queue: {QueueInfo.QueueNumber}\n" +
                         $"Client: {QueueInfo.ClientEmail}\n" +
                         $"Service: {QueueInfo.ServiceName}\n" +
                         $"Provider: {QueueInfo.ProviderName} ({QueueInfo.ProviderType})";

            using var qrGen = new QRCodeGenerator();
            using var qrData = qrGen.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrData);
            using var qrImage = qrCode.GetGraphic(20);
            using var ms = new MemoryStream();
            qrImage.Save(ms, ImageFormat.Png);
            QrCodeBase64 = Convert.ToBase64String(ms.ToArray());

            return Page();
        }

        public async Task<IActionResult> OnPostCallNextClientAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Message = await _queueService.CallNextClient(CallNextClientRequest);
            return Page();
        }
    }

    //public class QueueGeneratorModel : PageModel
    //{
    //    private readonly QueueService _queueService;

    //    public QueueGeneratorModel(QueueService queueService)
    //    {
    //        _queueService = queueService;
    //    }

    //    [BindProperty]
    //    public GenerateQueueNumberRequest GenerateQueueRequest { get; set; }

    //    [BindProperty]
    //    public CallNextClientRequest CallNextClientRequest { get; set; }

    //    public QueueInfoRequest QueueInfo { get; set; }
    //    public string QrCodeBase64 { get; set; }
    //    public string Message { get; set; }

    //    public IActionResult OnGet()
    //    {
    //        var clientId = HttpContext.Session.GetInt32("ClientId");
    //        var clientEmail = HttpContext.Session.GetString("ClientEmail");

    //        if (clientId == null || clientId == 0 || clientEmail == null)
    //        {
    //            return RedirectToPage("/Clients/ClientLogin");
    //        }

    //        GenerateQueueRequest = new GenerateQueueNumberRequest
    //        {
    //            ClientId = clientId.Value
    //        };

    //        return Page();
    //    }

    //    public async Task<IActionResult> OnPostGenerateQueueAsync()
    //    {
    //        var clientId = HttpContext.Session.GetInt32("ClientId");
    //        var clientEmail = HttpContext.Session.GetString("ClientEmail");


    //        if (clientId == null || clientId == 0 || clientEmail == null)
    //        {
    //            return RedirectToPage("/Clients/ClientLogin");
    //        }

    //        GenerateQueueRequest.ClientId = clientId.Value;
    //        QueueInfo = await _queueService.GenerateQueueInfo(GenerateQueueRequest);

    //        // Génération du contenu du QR Code
    //        var qrText = $"Queue: {QueueInfo.QueueNumber}\n" +
    //            $"Client: {QueueInfo.ClientEmail}\n" +
    //            $"Service: {QueueInfo.ServiceName}\n" +
    //            $"Provider: {QueueInfo.ProviderName} ({QueueInfo.ProviderType})";

    //        using var qrGen = new QRCodeGenerator();
    //        using var qrData = qrGen.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
    //        using var qrCode = new QRCode(qrData);
    //        using var qrImage = qrCode.GetGraphic(20);
    //        using var ms = new MemoryStream();
    //        qrImage.Save(ms, ImageFormat.Png);
    //        QrCodeBase64 = Convert.ToBase64String(ms.ToArray());

    //        return Page();
    //    }

    //    public async Task<IActionResult> OnPostCallNextClientAsync()
    //    {
    //        if (!ModelState.IsValid)
    //            return Page();

    //        Message = await _queueService.CallNextClient(CallNextClientRequest);
    //        return Page();
    //    }
    //}
}
