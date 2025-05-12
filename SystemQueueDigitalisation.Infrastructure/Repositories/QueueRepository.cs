using Microsoft.EntityFrameworkCore;
using System;
//using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Domain.Entities;
using SystemQueueDigitalisation.Infrastructure.Data;

namespace SystemQueueDigitalisation.Infrastructure.Repositories
{
    public class QueueRepository : RepositoryBase<Queue>, IQueueRepository
    {
        private readonly ApplicationDbContext _context;

        public QueueRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Queue> GetNextInQueueAsync(int serviceId)
        {
            return await _context.Queues
                .FirstOrDefaultAsync(q => q.ServiceId == serviceId && !q.IsServed);
        }

        public async Task<IEnumerable<Queue>> GetQueuesByProviderIdAsync(int providerId)
        {
            return await _context.Queues
                .Where(q => q.Service.ProviderId == providerId)
                .ToListAsync();
        }

        public async Task<Service> GetServiceWithProviderAsync(int serviceId)
        {
            return await _context.Services
                .Include(s => s.Provider)
                .FirstOrDefaultAsync(s => s.Id == serviceId);
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == clientId);
        }

        //public async Task<int> GetCountByClient(int clientId, DateTime date)
        //{
        //    var dateUTC = date.ToString("yyyy-MM-dd");
        //    var count = await _context.Queues
        //        .Where(c => c.ClientId == clientId && c.AppointmentTime.ToString() == dateUTC)
        //        .CountAsync();

        //    return count;
        //}

        public async Task<int> GetCountByClient(int clientId, DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            return await _context.Queues
                .Where(c => c.ClientId == clientId &&
                            c.AppointmentTime >= startDate &&
                            c.AppointmentTime < endDate)
                .CountAsync();
        }

        public async Task<int> GetQueuesByClientIdWithServiceIdByDateAsync(int clientId, int serviceId, DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            var queues = await _context.Queues
                .Where(q => q.ClientId == clientId &&
                        q.ServiceId == serviceId &&
                        q.AppointmentTime >= startDate &&
                        q.AppointmentTime <= endDate
                        )
                .CountAsync();

            return queues;
        }

        public async Task<IEnumerable<Queue>> GetQueuesByClientIdAsync(int clientId)
        {
            var queues = await _context.Queues
                .Where(q => q.ClientId == clientId)
                .Include(q => q.Service)
                .ThenInclude(s => s.Provider)
                .Include(q => q.Client)
                .ToListAsync();

            return queues;
        }

        public async Task<List<Queue>> GetQueuesByDateAsync(DateTime day)
        {
            var startOfDay = day.Date;
            var endOfDay = startOfDay.AddDays(1);

            return await _context.Queues
                .Where(q => q.CreatedAt >= startOfDay && q.CreatedAt < endOfDay && q.AppointmentTime == null)
                .ToListAsync();
        }


        public async Task<List<Queue>> GetAppointmentsByClientIdAsync(int clientId)
        {
            var queues = await _context.Queues
                .Where(q => q.ClientId == clientId)
                .OrderBy(q => q.AppointmentTime)
                .Include(q => q.Client)
                .Include(q => q.Service)
                .ThenInclude(s => s.Provider)
                .ToListAsync();

            return queues;
        }

    }
}
