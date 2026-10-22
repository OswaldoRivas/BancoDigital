
using BancoDigital.Application.Interfaces;
using BancoDigital.Infrastructure.Persistence;
using BancoDigital.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using BancoDigital.Application.Services;
using BancoDigital.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Registrar los controladores de la APIX
builder.Services.AddControllers();

// Habilitar OpenAPI
builder.Services.AddOpenApi();

// Configurar la base de datos SQLite
builder.Services.AddDbContext<BancoDigitalDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

/*conectando servicios con nuestra api*/

// Registrar el repositorio de clientes
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Registrar el servicio de clientes
builder.Services.AddScoped<ICustomerService, CustomerService>();


// Registrar el repositorio de cuentas bancarias
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();


// Registrar el servicio de cuentas bancaria
builder.Services.AddScoped<IBankAccountService, BankAccountService>();


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


var app = builder.Build();


app.UseExceptionHandler();


// Configuración de OpenAPI en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
