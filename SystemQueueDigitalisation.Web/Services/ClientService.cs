using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Web.Services
{
    public class ClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> RegisterClientAsync(RegisterClientRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/client/register", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result?.Message;
        }

        public async Task<ApiResponse> AuthenticateClientAsync(AuthenticateClientRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/client/authenticate", request);

            if (!response.IsSuccessStatusCode)
                return new ApiResponse { Message = "Invalid credentials.", ClientId = 0 };

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result;
        }

    }
}
