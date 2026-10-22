
namespace BancoDigital.API.DTOs;

public class TransactionResponseDto
{
    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public string Message { get; set; } = string.Empty;
}
