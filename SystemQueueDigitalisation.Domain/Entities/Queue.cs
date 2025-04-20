namespace SystemQueueDigitalisation.Domain.Entities
{
    public class Queue
    {
        public int Id { get; set; }
        public string QueueNumber { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? CalledAt { get; set; }
        public bool IsServed { get; set; }

        // Foreign keys to associate the queue with a client and service
        public int ClientId { get; set; }
        public int ServiceId { get; set; }

        // Navigation properties
        public virtual Client Client { get; set; }
        public virtual Service Service { get; set; }
    }
}
