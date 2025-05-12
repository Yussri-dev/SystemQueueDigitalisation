namespace SystemQueueDigitalisation.Api.RequestModel.QueueRequests
{
    public class BookAppointmentRequest
    {
        public int ClientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ServiceId { get; set; }
    }
}
