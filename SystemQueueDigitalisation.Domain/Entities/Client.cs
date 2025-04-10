namespace SystemQueueDigitalisation.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactInfo { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string AdressNumber { get; set; }
        public int BoxNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; } 

        // Navigation property: A client can have multiple queues
        public virtual ICollection<Queue> Queues { get; set; }
    }
}
