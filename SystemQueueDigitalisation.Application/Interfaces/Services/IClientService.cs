using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemQueueDigitalisation.Application.Interfaces.Services
{
    public interface IClientService
    {
        Task<int> RegisterClientAsync(string firstName, string lastName, string contactInfo, string adress, string city, string postCode,
            string adressNumber, int BoxNumber, DateTime BirthDate, string Email, string password, int age);
    }
}
