using BancoDigital.Application.Interfaces;
using BancoDigital.Domain.Entities;

namespace BancoDigital.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer> CreateCustomerAsync(
        string fullName,
        DateTime birthDate,
        string gender,
        decimal monthlyIncome)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException(
                "Customer name is required.");
        }

        if (birthDate.Date >= DateTime.Today)
        {
            throw new ArgumentException(
                "Birth date must be in the past.");
        }

        if (string.IsNullOrWhiteSpace(gender))
        {
            throw new ArgumentException(
                "Gender is required.");
        }

        if (monthlyIncome < 0)
        {
            throw new ArgumentException(
                "Monthly income cannot be negative.");
        }

        var customer = new Customer
        {
            FullName = fullName,
            BirthDate = birthDate,
            Gender = gender,
            MonthlyIncome = monthlyIncome
        };

        return await _customerRepository.AddAsync(customer);
    }
}