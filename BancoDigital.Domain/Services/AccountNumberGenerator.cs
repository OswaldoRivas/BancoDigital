using System.Security.Cryptography;

namespace BancoDigital.Domain.Services;

public class AccountNumberGenerator
{
    public string Generate()
    {
        string date = DateTime.Today.ToString("yyyyMMdd");

        int randomNumber = RandomNumberGenerator.GetInt32(0, 10000);

        string suffix = randomNumber.ToString("D4");

        return $"ACC-{date}-{suffix}";
    }
}