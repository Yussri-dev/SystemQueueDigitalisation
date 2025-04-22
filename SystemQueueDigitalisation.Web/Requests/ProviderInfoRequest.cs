namespace SystemQueueDigitalisation.Web.Requests
{
    public class ProviderInfoRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string Type { get; set; }

        public bool IsPaymentConfirmed { get; set; } = false;
    }
}
