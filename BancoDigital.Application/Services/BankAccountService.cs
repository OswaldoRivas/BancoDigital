
using BancoDigital.Application.Interfaces;
using BancoDigital.Domain.Entities;
using BancoDigital.Domain.Services;

namespace BancoDigital.Application.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _accountRepository;

    public BankAccountService(
        IBankAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }


    public async Task<BankAccount> CreateAccountAsync(
        int customerId,
        decimal initialBalance)
    {
        // Validar que el saldo inicial no sea negativo
        if (initialBalance < 0)
        {
            throw new ArgumentException(
                "El saldo inicial no puede ser negativo.");
        }

        // Comprobar que el cliente exista
        bool customerExists =
            await _accountRepository.CustomerExistsAsync(customerId);

        if (!customerExists)
        {
            throw new ArgumentException(
                "El cliente indicado no existe.");
        }

        // Preparar la generación del número de cuenta
        string accountNumber = string.Empty;
        bool uniqueNumberFound = false;

        // Intentar generar un número único hasta 100 veces
        for (int attempts = 0; attempts < 100; attempts++)
        {
            accountNumber = new AccountNumberGenerator().Generate();

            // Comprobar que el número no esté registrado
            if (!await _accountRepository
                .AccountNumberExistsAsync(accountNumber))
            {
                uniqueNumberFound = true;
                break;
            }
        }

        // Si no encontramos un número disponible
        if (!uniqueNumberFound)
        {
            throw new InvalidOperationException(
                "No fue posible generar un número de cuenta único.");
        }

        // Crear la cuenta bancaria
        var account = new BankAccount(
            accountNumber,
            customerId,
            initialBalance);

        // Guardar la cuenta en SQLite
        return await _accountRepository.AddAsync(account);
    }


    public async Task<decimal> GetBalanceAsync(
        string accountNumber)
    {
        var account = await _accountRepository
            .GetByAccountNumberAsync(accountNumber);

        if (account == null)
        {
            throw new KeyNotFoundException(
                "La cuenta bancaria no existe.");
        }

        return account.Balance;
    }



    public async Task<decimal> DepositAsync(
        string accountNumber,
        decimal amount)
    {
        // Validar que el monto sea positivo
        if (amount <= 0)
        {
            throw new ArgumentException(
                "El monto del depósito debe ser mayor que cero.");
        }

        // Buscar la cuenta bancaria
        var account = await _accountRepository
            .GetByAccountNumberAsync(accountNumber);

        // Verificar que exista
        if (account == null)
        {
            throw new KeyNotFoundException(
                "La cuenta bancaria no existe.");
        }

        // Aumentar el saldo de la cuenta
        account.Deposit(amount);


        var transaction = new BankTransaction
        {
            BankAccountId = account.Id,
            TransactionType = "Deposit",
            Amount = amount,
            CreatedAt = DateTime.UtcNow,
            BalanceAfterTransaction = account.Balance
        };


        await _accountRepository.SaveTransactionAsync(transaction);

        // Devolver el nuevo saldo
        return account.Balance;
    }


    public async Task<decimal> WithdrawAsync(
        string accountNumber,
        decimal amount)
    {
        // Validar que el monto sea positivo
        if (amount <= 0)
        {
            throw new ArgumentException(
                "El monto del retiro debe ser mayor que cero.");
        }

        // Buscar la cuenta bancaria
        var account = await _accountRepository
            .GetByAccountNumberAsync(accountNumber);

        // Verificar que la cuenta exista
        if (account == null)
        {
            throw new KeyNotFoundException(
                "La cuenta bancaria no existe.");
        }

        // Retirar dinero y validar fondos suficientes
        account.Withdraw(amount);

        // Registrar el movimiento bancario
        var transaction = new BankTransaction
        {
            BankAccountId = account.Id,
            TransactionType = "Withdrawal",
            Amount = amount,
            CreatedAt = DateTime.UtcNow,
            BalanceAfterTransaction = account.Balance
        };

        // Guardar el retiro y el saldo actualizado
        await _accountRepository.SaveTransactionAsync(transaction);

        // Devolver el nuevo saldo
        return account.Balance;
    }



    public async Task<List<BankTransaction>> GetTransactionHistoryAsync(
        string accountNumber)
    {
        var account = await _accountRepository
            .GetByAccountNumberAsync(accountNumber);

        if (account == null)
        {
            throw new KeyNotFoundException(
                "La cuenta bancaria no existe.");
        }

        return await _accountRepository
            .GetTransactionsAsync(account.Id);
    }


}
