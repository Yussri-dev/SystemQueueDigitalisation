using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Domain.Entities;
using SystemQueueDigitalisation.Domain.Helpers;

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

    public async Task<bool> AuthenticateProviderAsync(string email, string password)
    {
        var provider = await _providerRepository.GetByEmailAsync(email);
        if (provider == null)
            return false;

        var trimmedPassword = password.Trim();

        return PasswordHelper.VerifyPassword(trimmedPassword, provider.PasswordHash);
    }
}