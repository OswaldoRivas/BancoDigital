
namespace BancoDigital.API.DTOs;

public class BankTransactionResponseDto
{
    public int Id { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal BalanceAfterTransaction { get; set; }
}