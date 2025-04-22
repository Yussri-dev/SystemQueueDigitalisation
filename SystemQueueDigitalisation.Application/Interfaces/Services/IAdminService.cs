using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Domain.Entities;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface IAdminService
    {
        Task<List<Provider>> GetPendingPaymentsAsync();
        Task ConfirmPaymentManuallyAsync(int providerId);
    }
}
