using SystemQueueDigitalisation.Web.Requests;
using SystemQueueDigitalisation.Web.Requests.Appointments;

namespace SystemQueueDigitalisation.Web.Services
{
    public class QueueService
    {
        private readonly HttpClient _httpClient;

        public QueueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<QueueInfoRequest> GenerateQueueInfo(GenerateQueueNumberRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Queue/generate", request);
            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var wrapper = await response.Content.ReadFromJsonAsync<QueueApiResponse>();
            return wrapper.Queue;
        }


        public async Task<string> CallNextClient(CallNextClientRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Queue/call-next", request);

            if (!response.IsSuccessStatusCode)
                return "Error credentials.";

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result?.Message;
        }

        public async Task<List<QueueInfoRequest>> GetQueueStatusAsync(int clientId)
        {
            var response = await _httpClient.GetAsync($"api/queue/status/{clientId}");
            if (!response.IsSuccessStatusCode)
                return new List<QueueInfoRequest>();


            return await response.Content.ReadFromJsonAsync<List<QueueInfoRequest>>();
        }

        public async Task<ApiResponse> BookAppointmentAsync(BookAppointmentRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/queue/book-appointment", request);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse
                {
                    Message = "Failed to book appointment.",
                    Success = false
                };
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

            if (apiResponse == null)
            {
                return new ApiResponse
                {
                    Message = "Error: Null response from API.",
                    Success = false
                };
            }
            return apiResponse;
        }

        public async Task<ApiResponse> GenerateAppointmentsForDayAsync(GenerateAppointmentsForDayRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/queue/generate-appointments", request);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse { Message = "Failed to generate appointments.", Success = false };
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }

        public async Task<List<QueueInfoRequest>> GetAppointmentsByClientAsync(int clientId)
        {
            var response = await _httpClient.GetAsync($"api/queue/appointments/{clientId}");

            if (!response.IsSuccessStatusCode)
                return new List<QueueInfoRequest>();

            return await response.Content.ReadFromJsonAsync<List<QueueInfoRequest>>();
        }

        public async Task<int> GetCountByClient(int clientId, DateTime date)
        {
            var response = await _httpClient.GetAsync($"api/Queue/count/{clientId}?date={date}");

            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }

            var content = await response.Content.ReadAsStringAsync();

            if (int.TryParse(content, out int count))
            {
                return count;
            }

            return 0;
        }

    }
}
