using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Domain.Entities;

namespace SystemQueueDigitalisation.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IProviderRepository _providerRepository;
        //private readonly INotificationService _notificationService;

        public AdminService(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<List<Provider>> GetPendingPaymentsAsync()
        {
            var allProviders = await _providerRepository.GetAllAsync();
            return allProviders.Where(p => !p.IsPaymentConfirmed).ToList();
        }

        public async Task ConfirmPaymentManuallyAsync(int providerId)
        {
            var provider = await _providerRepository.GetByIdAsync(providerId);
            if (provider == null) throw new Exception("Provider not found");

            provider.IsPaymentConfirmed = true;
            await _providerRepository.UpdateAsync(provider);
        }
    }
}
