namespace SystemQueueDigitalisation.Api.Controllers
{
    public partial class ServiceController
    {
        public class RegisterServiceRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int ProviderId { get; set; }
        }
    }
}
