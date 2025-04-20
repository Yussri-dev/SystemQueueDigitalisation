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

        public async Task<ApiResponse> AuthenticateProviderAsync(AuthenticateProviderRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/provider/authenticate", request);

            if (!response.IsSuccessStatusCode)
                return new ApiResponse { Message = "Invalid credentials.", ProviderId = 0 };

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result;
        }

        //public async Task<List<QueueInfo>> GetTodayQueuesAsync(int providerId)
        //{
        //    var response = await _httpClient.GetAsync($"api/provider/{providerId}/queues/today");
        //    if (!response.IsSuccessStatusCode)
        //        return new List<QueueInfo>();

        //    return await response.Content.ReadFromJsonAsync<List<QueueInfo>>();
        //}

        public async Task<List<QueueInfo>> GetTodayQueuesAsync(int providerId)
        {
            try
            {
                // Capital 'P' to match controller name
                var response = await _httpClient.GetAsync($"api/Provider/{providerId}/queues/today");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API error: {response.StatusCode}, {errorContent}");
                    return new List<QueueInfo>();
                }

                // Get the raw JSON to see what's coming back
                var jsonContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API returned JSON: {jsonContent}");

                // Use case-insensitive deserialization
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<List<QueueInfo>>(options);
                Console.WriteLine($"Deserialized {result?.Count ?? 0} queues");
                return result ?? new List<QueueInfo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetTodayQueuesAsync: {ex.Message}");
                return new List<QueueInfo>();
            }
        }


        public async Task<List<QueueInfo>> GetQueuesByDateAsync(int providerId, DateTime date)
        {
            var formattedDate = date.ToString("yyyy-MM-dd");
            var response = await _httpClient.GetAsync($"api/provider/{providerId}/queues?date={formattedDate}");
            if (!response.IsSuccessStatusCode)
                return new List<QueueInfo>();

            return await response.Content.ReadFromJsonAsync<List<QueueInfo>>();
        }



        public async Task<bool> MarkAsServedAsync(int queueId)
        {
            var response = await _httpClient.PutAsync($"api/provider/queue/{queueId}/serve", null);
            return response.IsSuccessStatusCode;
        }

    }
}
