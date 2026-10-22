
namespace BancoDigital.Domain.Entities;

public class BankTransaction
{
    public int Id { get; set; }

    public int BankAccountId { get; set; }

    public BankAccount BankAccount { get; set; } = null!;

    public string TransactionType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal BalanceAfterTransaction { get; set; }
}
