namespace SystemQueueDigitalisation.Web.Requests.Appointments
{
    public class QueueAppointmentResponse
    {
        public int ClientId { get; set; }
        public int ProviderId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

    }
}
