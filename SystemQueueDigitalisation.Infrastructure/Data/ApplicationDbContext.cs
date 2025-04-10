using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Domain.Entities;

namespace SystemQueueDigitalisation.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships and constraints here if needed
            modelBuilder.Entity<Queue>()
                .HasOne(q => q.Client)
                .WithMany(c => c.Queues)
                .HasForeignKey(q => q.ClientId);

            modelBuilder.Entity<Queue>()
                .HasOne(q => q.Service)
                .WithMany(s => s.Queues)
                .HasForeignKey(q => q.ServiceId);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.Provider)
                .WithMany(p => p.Services)
                .HasForeignKey(s => s.ProviderId);

        }
    }
}
