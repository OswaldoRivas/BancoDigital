using BancoDigital.Domain.Services;
using Xunit;

namespace BancoDigital.Tests;

public class AccountNumberGeneratorTests
{
    [Fact]
    public void Generate_ShouldHaveCorrectFormat()
    {
        // Arrange: preparar el generador
        var generator = new AccountNumberGenerator();

        // Act: generar el número de cuenta
        string accountNumber = generator.Generate();

        // Assert: comprobar el formato
        string expectedPrefix =
            $"ACC-{DateTime.Today:yyyyMMdd}-";

        Assert.StartsWith(expectedPrefix, accountNumber);

        Assert.Equal(17, accountNumber.Length);

        string suffix = accountNumber.Substring(13, 4);

        Assert.Matches(@"^\d{4}$", suffix);
    }


    [Fact]
    public void Generate_ShouldProduceUniqueAccountNumbers()
    {
        // Arrange: preparar el generador
        var generator = new AccountNumberGenerator();

        // Almacenar números generados sin duplicados
        var accountNumbers = new HashSet<string>();

        // Act: generar 100 números de cuenta
        for (int i = 0; i < 100; i++)
        {
            string accountNumber = generator.Generate();

            // Assert: comprobar que no esté repetido
            Assert.True(
                accountNumbers.Add(accountNumber),
                $"Número de cuenta duplicado: {accountNumber}");
        }
    }


}