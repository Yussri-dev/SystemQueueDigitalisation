namespace SystemQueueDigitalisation.Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } // Name of the service (e.g., "Consultation", "Loan Approval")
        public string Description { get; set; } // Description of the service

        // Foreign key to associate the service with a provider
        public int ProviderId { get; set; }

        // Navigation properties
        public virtual Provider Provider { get; set; }
        public virtual ICollection<Queue> Queues { get; set; } // Queues associated with the service
    }
}
