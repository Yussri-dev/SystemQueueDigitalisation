namespace SystemQueueDigitalisation.Domain.Entities
{
    public class Queue
    {
        public int Id { get; set; }
        public string QueueNumber { get; set; } // Unique identifier for the ticket (e.g., "Q123")
        public DateTime CreatedAt { get; set; } // Timestamp when the ticket was created
        public DateTime? CalledAt { get; set; } // Timestamp when the client was called
        public bool IsServed { get; set; } // Indicates whether the client has been served

        // Foreign keys to associate the queue with a client and service
        public int ClientId { get; set; }
        public int ServiceId { get; set; }

        // Navigation properties
        public virtual Client Client { get; set; }
        public virtual Service Service { get; set; }
    }
}
