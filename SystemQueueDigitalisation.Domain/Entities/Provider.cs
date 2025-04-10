using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemQueueDigitalisation.Domain.Entities
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Type of provider (e.g., "Doctor", "Bank", "Kiosk")
        public string Type { get; set; }

        // Navigation properties
        public virtual ICollection<Service> Services { get; set; }
    }
}
