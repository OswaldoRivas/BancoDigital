
using BancoDigital.Application.Interfaces;
using BancoDigital.Domain.Entities;
using BancoDigital.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BancoDigital.Infrastructure.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly BancoDigitalDbContext _context;

    public BankAccountRepository(BancoDigitalDbContext context)
    {
        _context = context;
    }

    public async Task<BankAccount> AddAsync(BankAccount account)
    {
        _context.BankAccounts.Add(account);
        await _context.SaveChangesAsync();

        return account;
    }

    public async Task<BankAccount?> GetByAccountNumberAsync(
        string accountNumber)
    {
        return await _context.BankAccounts
            .FirstOrDefaultAsync(a =>
                a.AccountNumber == accountNumber);
    }

    public async Task<bool> AccountNumberExistsAsync(
        string accountNumber)
    {
        return await _context.BankAccounts
            .AnyAsync(a =>
                a.AccountNumber == accountNumber);
    }

    public async Task<bool> CustomerExistsAsync(int customerId)
    {
        return await _context.Customers
            .AnyAsync(c => c.Id == customerId);
    }


    public async Task SaveTransactionAsync(
        BankTransaction transaction)
    {
        // Agregar el movimiento
        _context.BankTransactions.Add(transaction);

        // Guardar la transacción y el saldo actualizado
        await _context.SaveChangesAsync();
    }

    public async Task<List<BankTransaction>> GetTransactionsAsync(
        int bankAccountId)
    {
        // Consultar los movimientos de una cuenta
        return await _context.BankTransactions
            .AsNoTracking()
            .Where(t => t.BankAccountId == bankAccountId)
            .OrderBy(t => t.CreatedAt)
            .ThenBy(t => t.Id)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
