using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Domain.Entities;
using SystemQueueDigitalisation.Application.Helpers;

namespace SystemQueueDigitalisation.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<int> RegisterClientAsync(string firstName, string lastName, string contactInfo,string adress,string city, 
            string postCode,
            string adressNumber, int boxNumber, DateTime birthDate,string email, string password,int age)
        {
            var existingClient = await _clientRepository.GetByContactInfoAsync(contactInfo);
            if (existingClient != null)
                return existingClient.Id;

            var client = new Client
            {
                FirstName = firstName,
                LastName = lastName,
                ContactInfo = contactInfo,
                Adress = adress,
                City = city,
                PostCode = postCode,
                AdressNumber = adressNumber,
                BoxNumber = boxNumber,
                BirthDate = birthDate,
                Email = email,
                PasswordHash = PasswordHelper.HashPassword(password),
                Age = age
            };

            await _clientRepository.AddAsync(client);
            return client.Id;
        }
    }
}
