
using BancoDigital.Application.Interfaces;
using BancoDigital.Domain.Entities;
using BancoDigital.Infrastructure.Persistence;

namespace BancoDigital.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly BancoDigitalDbContext _context;

    public CustomerRepository(BancoDigitalDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);

        await _context.SaveChangesAsync();

        return customer;
    }
}
