using BancoDigital.Domain.Entities;

namespace BancoDigital.Application.Interfaces;

public interface ICustomerService
{
    Task<Customer> CreateCustomerAsync(
        string fullName,
        DateTime birthDate,
        string gender,
        decimal monthlyIncome);
}