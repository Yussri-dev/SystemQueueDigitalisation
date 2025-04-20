namespace SystemQueueDigitalisation.Web.Requests
{
    public class QueueInfoRequest
    {
        public string QueueNumber { get; set; }
        public string ClientEmail { get; set; }
        public string ServiceName { get; set; }
        public string ProviderName { get; set; }
        public string ProviderType { get; set; }

    }
}
