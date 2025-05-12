namespace SystemQueueDigitalisation.Web.Requests
{
    public class ApiResponse
    {
        public int ClientId { get; set; }
        public int ProviderId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

    }


}
