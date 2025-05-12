using SystemQueueDigitalisation.Web.Dtos;
using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionService _sessionService;

        public AuthService(HttpClient httpClient, ISessionService sessionService)
        {
            _httpClient = httpClient;
            _sessionService = sessionService;
        }

        public async Task<AuthResponseRequest> LoginAsync(LoginRequest loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);

            if (!response.IsSuccessStatusCode)
            {
                return new AuthResponseRequest
                {
                    IsSuccess = false,
                    ErrorMessage = "Erreur lors de la connexion"
                };
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseRequest>();

            if (authResponse.IsSuccess)
            {
                // Enregistrer le token dans la session
                _sessionService.SetToken(authResponse.Token);
                _sessionService.SetUserRole(authResponse.Roles);
            }

            return authResponse;
        }

        public void Logout()
        {
            _sessionService.ClearSession();
        }
    }
}
