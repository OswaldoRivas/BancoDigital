
namespace BancoDigital.API.DTOs;

public class CreateBankAccountRequestDto
{
    public int CustomerId { get; set; }

    public decimal InitialBalance { get; set; }
}
