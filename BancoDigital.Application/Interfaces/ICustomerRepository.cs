using BancoDigital.Domain.Entities;

namespace BancoDigital.Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> AddAsync(Customer customer);
}