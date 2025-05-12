namespace SystemQueueDigitalisation.Api.RequestModel.QueueRequests
{
    public class GenerateAppointmentsForDayRequest
    {
        public DateTime Day { get; set; }
        public int Hour { get; set; }
        public int appointmentTime { get; set; }
    }
}
