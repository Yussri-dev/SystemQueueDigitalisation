using Microsoft.AspNetCore.Identity;

namespace SystemQueueDigitalisation.Web.Requests
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
