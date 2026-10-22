
using BancoDigital.Domain.Entities;

namespace BancoDigital.Application.Interfaces;

public interface IBankAccountRepository
{
    Task<BankAccount> AddAsync(BankAccount account);

    Task<BankAccount?> GetByAccountNumberAsync(
        string accountNumber);

    Task<bool> AccountNumberExistsAsync(
        string accountNumber);

    Task<bool> CustomerExistsAsync(int customerId);

    //Guarda la transaccion y actualiza el saldo
    Task SaveTransactionAsync(BankTransaction transaction);

    Task<List<BankTransaction>> GetTransactionsAsync(
        int bankAccountId);

    Task SaveChangesAsync();

}