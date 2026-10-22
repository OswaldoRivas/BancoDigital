
using BancoDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BancoDigital.Infrastructure.Persistence;

public class BancoDigitalDbContext : DbContext
{
    public BancoDigitalDbContext(
        DbContextOptions<BancoDigitalDbContext> options)
        : base(options)
    {
    }

    //Propiedades creadas EFC

    public DbSet<Customer> Customers { get; set; }

    public DbSet<BankAccount> BankAccounts { get; set; }

    public DbSet<BankTransaction> BankTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BankAccount>()
            .HasIndex(account => account.AccountNumber)
            .IsUnique(); //valida sea unico su numero de cuenta
    }
}
