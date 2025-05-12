using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Domain.Dtos;
using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface IQueueService
    {
        Task<QueueInfoRequest> GenerateQueueInfoAsync(int clientId, int serviceId);
        Task<List<QueueInfoRequest>> GetQueueStatusAsync(int clientId);
        Task<int> GetCountByClient(int clientId, DateTime date);
        Task CallNextClientAsync(int serviceId);

        //Appointments
        Task<int> BookAppointmentAsync(int clientId, DateTime appointmentDate, int serviceId);
        Task GenerateAppointmentsForDayAsync(DateTime day, int hourStart, int duration);
        Task<List<QueueInfoRequest>> GetAppointmentsByClientAsync(int clientId);
    }
}
