namespace SystemQueueDigitalisation.Web.Dtos
{
    public class AuthResponseRequest
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public string ErrorMessage { get; set; }
    }
}
