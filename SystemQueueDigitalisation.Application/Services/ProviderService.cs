using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Domain.Entities;
using SystemQueueDigitalisation.Domain.Helpers;
using System.Data.Entity;
using SystemQueueDigitalisation.Domain.Dtos;

public class ProviderService : IProviderService
{
    private readonly IProviderRepository _providerRepository;

    public ProviderService(IProviderRepository providerRepository)
    {
        _providerRepository = providerRepository;
    }

    public async Task RegisterProviderAsync(string name, string email, string password, string type)
    {
        var existingProvider = await _providerRepository.GetByEmailAsync(email);
        if (existingProvider != null)
            throw new Exception("A provider with this email already exists.");

        var provider = new Provider
        {
            Name = name,
            Email = email,
            PasswordHash = PasswordHelper.HashPassword(password.Trim()),
            Type = type
        };

        await _providerRepository.AddAsync(provider);
    }


    public async Task<Provider?> AuthenticateProviderAsync(string email, string password)
    {
        var provider = await _providerRepository.GetByEmailAsync(email);

        if (provider == null || provider.Email != email)
            return null;

        var trimmedPassword = password.Trim();

        return PasswordHelper.VerifyPassword(trimmedPassword, provider.PasswordHash) ? provider : null;
    }

    public async Task<List<QueueInfo>> GetTodayQueueAsync(int providerId)
    {
        var provider = await _providerRepository.GetWithServicesAsync(providerId);

        if (provider == null)
            throw new Exception("Provider not found.");

        var serviceIds = provider.Services.Select(s => s.Id).ToList();

        var today = DateTime.Today;

        var queue = await _providerRepository.GetQueueByServicesAndDateAsync(serviceIds, today);

        return queue.Select(q => new QueueInfo
        {
            QueueId = q.Id,
            ClientEmail = q.Client.Email,
            ServiceName = q.Service.Name,
            QueueNumber = q.QueueNumber,
            CreatedAt = q.CreatedAt,
            IsServed = q.IsServed
        }).ToList();
    }

    public async Task MarkAsServedAsync(int queueId)
    {
        var queue = await _providerRepository.GetQueueByIdAsync(queueId);

        if (queue == null)
            throw new Exception("Queue entry not found.");

        queue.IsServed = true;
        queue.CalledAt = DateTime.Now;

        await _providerRepository.UpdateQueueAsync(queue);
    }

    public async Task<List<QueueInfo>> GetQueuesByDateAsync(int providerId, DateTime date)
    {
        var provider = await _providerRepository.GetWithServicesAsync(providerId);
        if (provider == null)
            throw new Exception("Provider not found.");

        var serviceIds = provider.Services.Select(s => s.Id).ToList();
        var queues = await _providerRepository.GetQueueByServicesAndDateAsync(serviceIds, date);

        return queues.Select(q => new QueueInfo
        {
            QueueId = q.Id,
            QueueNumber = q.QueueNumber,
            ClientEmail = q.Client.Email,
            ServiceName = q.Service.Name,
            CreatedAt = q.CreatedAt,
            IsServed = q.IsServed
        }).ToList();
    }

    public async Task<List<ServiceDto>> GetServicesByProvider(int providerId)
    {
        var services = await _providerRepository.GetServicesByProviderAsync(providerId);

        return services.Select(s => new ServiceDto
        {
            Id = s.Id,
            Name = s.Name
        }).ToList();
    }



}