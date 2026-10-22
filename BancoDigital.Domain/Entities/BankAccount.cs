using System;
using System.Collections.Generic;
using System.Text;
using BancoDigital.Domain.Exceptions;


namespace BancoDigital.Domain.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; } = string.Empty;

        public decimal Balance { get; private set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        private BankAccount()
        {
        }

        public BankAccount(
                string accountNumber,
                int customerId,
                decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException(
                    "Account number is required.");
            }

            if (customerId <= 0)
            {
                throw new ArgumentException(
                    "Customer ID must be valid.");
            }

            if (initialBalance < 0)
            {
                throw new ArgumentException(
                    "Initial balance cannot be negative.");
            }

            AccountNumber = accountNumber;
            CustomerId = customerId;
            Balance = initialBalance;
        }

        //Metodo deposito aumenta el saldo de la cuenta.
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(
                    "Deposit amount must be greater than zero.");
            }

            Balance += amount;
        }

        //Disminuye el saldo de la cuenta.
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(
                    "Withdrawal amount must be greater than zero.");
            }

            if (amount > Balance)
            {
                throw new BusinessException(
                    "Fondos insuficientes para realizar el retiro.");
            }


            Balance -= amount;
        }

    }
}
