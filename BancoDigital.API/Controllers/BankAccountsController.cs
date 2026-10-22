
using BancoDigital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BancoDigital.API.DTOs;

namespace BancoDigital.API.Controllers;

[ApiController]
[Route("api/accounts")]
public class BankAccountsController : ControllerBase
{
    private readonly IBankAccountService _accountService;

    public BankAccountsController(
        IBankAccountService accountService)
    {
        _accountService = accountService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateAccount(
        [FromBody] CreateBankAccountRequestDto request)
    {
        // Solicitar al servicio que cree la cuenta
        var account = await _accountService.CreateAccountAsync(
            request.CustomerId,
            request.InitialBalance);

        
        var response = new CreateBankAccountResponseDto
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            CustomerId = account.CustomerId,
            Balance = account.Balance
        };


        return Created(
            $"/api/accounts/{account.AccountNumber}/balance",
            response);
    }


    [HttpGet("{accountNumber}/balance")]
    public async Task<IActionResult> GetBalance(
        string accountNumber)
    {
        var balance = await _accountService
            .GetBalanceAsync(accountNumber);

        var response = new BalanceResponseDto
        {
            AccountNumber = accountNumber,
            Balance = balance
        };

        return Ok(response);
    }



    [HttpPost("{accountNumber}/deposit")]
    public async Task<IActionResult> Deposit(
        string accountNumber,
        [FromBody] TransactionRequestDto request)
    {
        // Ejecutar el depósito mediante el servicio
        var balance = await _accountService.DepositAsync(
            accountNumber, request.Amount);

        // Preparar la respuesta
        var response = new TransactionResponseDto
        {
            AccountNumber = accountNumber,
            Balance = balance,
            Message = "Depósito realizado correctamente."
        };

        return Ok(response);
    }



    [HttpPost("{accountNumber}/withdraw")]
    public async Task<IActionResult> Withdraw(
        string accountNumber,
        [FromBody] TransactionRequestDto request)
    {
        // Ejecutar el retiro mediante el servicio
        var balance = await _accountService.WithdrawAsync(
            accountNumber, request.Amount);

        // Preparar la respuesta
        var response = new TransactionResponseDto
        {
            AccountNumber = accountNumber,
            Balance = balance,
            Message = "Retiro realizado correctamente."
        };

        return Ok(response);
    }



    [HttpGet("{accountNumber}/transactions")]
    public async Task<IActionResult> GetTransactions(
        string accountNumber)
    {
        // Consultar el historial mediante el servicio
        var transactions = await _accountService
            .GetTransactionHistoryAsync(accountNumber);

        // Convertir las entidades a DTOs
        var response = transactions.Select(t =>
            new BankTransactionResponseDto
            {
                Id = t.Id,
                TransactionType = t.TransactionType,
                Amount = t.Amount,
                CreatedAt = t.CreatedAt,
                BalanceAfterTransaction = t.BalanceAfterTransaction
            }).ToList();

        return Ok(response);
    }



}
