namespace SystemQueueDigitalisation.Web.Services
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string TokenKey = "AuthToken";
        private const string RolesKey = "UserRoles";

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Session.SetString(TokenKey, token);
        }

        public string GetToken()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString(TokenKey);
        }

        public void SetUserRole(List<string> roles)
        {
            var rolesJson = System.Text.Json.JsonSerializer.Serialize(roles);
            _httpContextAccessor.HttpContext?.Session.SetString(RolesKey, rolesJson);
        }

        public List<string> GetUserRoles()
        {
            var rolesJson = _httpContextAccessor.HttpContext?.Session.GetString(RolesKey);
            if (string.IsNullOrEmpty(rolesJson))
                return new List<string>();

            return System.Text.Json.JsonSerializer.Deserialize<List<string>>(rolesJson);
        }

        public void ClearSession()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }
    }
}
