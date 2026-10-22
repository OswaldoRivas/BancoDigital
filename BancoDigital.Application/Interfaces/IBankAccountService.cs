
using BancoDigital.Domain.Entities;

namespace BancoDigital.Application.Interfaces;

public interface IBankAccountService
{
    Task<BankAccount> CreateAccountAsync(
        int customerId,
        decimal initialBalance);

    Task<decimal> GetBalanceAsync(
        string accountNumber);

    Task<decimal> DepositAsync(
        string accountNumber,
        decimal amount);

    Task<decimal> WithdrawAsync(
        string accountNumber,
        decimal amount);

    Task<List<BankTransaction>> GetTransactionHistoryAsync(
        string accountNumber);

}
