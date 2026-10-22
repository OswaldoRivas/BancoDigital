
namespace BancoDigital.API.DTOs;

public class BalanceResponseDto
{
    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}
