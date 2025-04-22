using System.Net.Http;
using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Web.Services
{
    public class AdminService
    {

        private readonly HttpClient _httpClient;
        private readonly ISessionService _sessionService;

        public AdminService(HttpClient httpClient, ISessionService sessionService)
        {
            _httpClient = httpClient;
            _sessionService = sessionService;
        }

        public async Task<bool> ConfirmManualPayment(int providerId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PostAsync($"api/admin/{providerId}/confirm-payment", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ProviderInfoRequest>> GetPendingPayments()
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"api/admin/pending-payments");
            if (!response.IsSuccessStatusCode)
                return new List<ProviderInfoRequest>();
            return await response.Content.ReadFromJsonAsync<List<ProviderInfoRequest>>();
        }

        private void AddAuthorizationHeader()
        {
            var token = _sessionService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

    }
}
