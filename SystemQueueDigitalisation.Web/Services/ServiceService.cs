using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Web.Services
{
    public class ServiceService
    {
        private readonly HttpClient _httpClient;

        public ServiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> RegisterServiceAsync(RegisterServiceRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/service/register", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result?.Message;
        }
       
    }
}
