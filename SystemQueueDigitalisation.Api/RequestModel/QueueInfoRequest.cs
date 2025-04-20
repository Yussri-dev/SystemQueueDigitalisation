namespace SystemQueueDigitalisation.Api.RequestModel
{
    public class QueueInfoRequest
    {
        public int QueueId { get; set; }
        public string QueueNumber { get; set; }
        public string ClientEmail { get; set; }
        public string ServiceName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsServed { get; set; }
    }

}
