namespace SystemQueueDigitalisation.Api.RequestModel
{
    public class AuthenticateClientRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
