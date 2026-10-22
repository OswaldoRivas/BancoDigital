
using BancoDigital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BancoDigital.API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(
        [FromBody] CreateCustomerRequest request)
    {
        var customer = await _customerService.CreateCustomerAsync(
            request.FullName,
            request.BirthDate,
            request.Gender,
            request.MonthlyIncome
        );

        return Created(
            $"/api/customers/{customer.Id}",
            customer
        );
    }
}

public class CreateCustomerRequest
{
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = string.Empty;
    public decimal MonthlyIncome { get; set; }
}
