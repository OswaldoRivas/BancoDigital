using BancoDigital.Domain.Entities;
using BancoDigital.Domain.Exceptions;
using Xunit;

namespace BancoDigital.Tests;

public class BankAccountTests
{
    [Fact]
    public void Deposit_ShouldIncreaseBalance()
    {
        // Arrange: preparar los datos
        var account = new BankAccount(
            "ACC-20261021-1234",
            1,
            1000m);

        // Act: ejecutar el depósito
        account.Deposit(500m);

        // Assert: comprobar el resultado
        Assert.Equal(1500m, account.Balance);
    }

    [Fact]
    public void Withdraw_WithSufficientFunds_ShouldDecreaseBalance()
    {
        // Arrange: preparar una cuenta con C$1,000
        var account = new BankAccount(
            "ACC-20261021-1234",
            1,
            1000m);

        // Act: retirar C$300
        account.Withdraw(300m);

        // Assert: verificar el saldo restante
        Assert.Equal(700m, account.Balance);
    }

    [Fact]
    public void Withdraw_WithInsufficientFunds_ShouldThrowException()
    {
        // Arrange: preparar una cuenta con C$1,000
        var account = new BankAccount(
            "ACC-20261021-1234",
            1,
            1000m);

        // Act y Assert: verificar que se rechace el retiro
        var exception = Assert.Throws<BusinessException>(
            () => account.Withdraw(1500m));

        // Verificar el mensaje de la excepción
        Assert.Equal(
            "Fondos insuficientes para realizar el retiro.",
            exception.Message);

        // Verificar que el saldo permanezca intacto
        Assert.Equal(1000m, account.Balance);
    }


}