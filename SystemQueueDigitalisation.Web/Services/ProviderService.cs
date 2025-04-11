using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Web.Services
{
    public class ProviderService
    {
        private readonly HttpClient _httpClient;

        public ProviderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> RegisterProviderAsync(RegisterProviderRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/provider/register", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result?.Message;
        }

        public async Task<string> AuthenticateProviderAsync(AuthenticateProviderRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/provider/authenticate", request);
            if (!response.IsSuccessStatusCode)
                return "Invalid credentials.";

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result?.Message;
        }
    }
}
