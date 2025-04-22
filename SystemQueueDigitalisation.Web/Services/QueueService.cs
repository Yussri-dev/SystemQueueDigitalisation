using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Web.Services
{
    public class QueueService
    {
        private readonly HttpClient _httpClient;

        public QueueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateQueueNumber(GenerateQueueNumberRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Queue/generate", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result?.Message;
        }

        public async Task<QueueInfoRequest> GenerateQueueInfo(GenerateQueueNumberRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Queue/generate", request);
            response.EnsureSuccessStatusCode();

            var wrapper = await response.Content.ReadFromJsonAsync<QueueApiResponse>();
            return wrapper.QueueNumber;
        }


        public async Task<string> CallNextClient(CallNextClientRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Queue/call-next", request);

            if (!response.IsSuccessStatusCode)
                return "Error credentials.";

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result?.Message;
        }


    }
}
