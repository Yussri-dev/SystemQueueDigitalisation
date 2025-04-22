using System.ComponentModel.DataAnnotations;

namespace SystemQueueDigitalisation.Web.Dtos
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
