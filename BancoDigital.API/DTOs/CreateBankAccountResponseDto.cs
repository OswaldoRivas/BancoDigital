
namespace BancoDigital.API.DTOs;

public class CreateBankAccountResponseDto
{
    public int Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public decimal Balance { get; set; }
}
