namespace SystemQueueDigitalisation.Web.Requests
{
    public class GenerateQueueNumberRequest
    {
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
