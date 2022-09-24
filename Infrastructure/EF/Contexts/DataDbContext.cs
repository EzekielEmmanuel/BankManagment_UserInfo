using Infrastructure.BankAccounts.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Contexts;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
    }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }
}