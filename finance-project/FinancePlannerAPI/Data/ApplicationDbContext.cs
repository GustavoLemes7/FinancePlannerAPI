namespace FinancePlannerAPI.Data;
using Microsoft.EntityFrameworkCore;
using FinancePlannerAPI.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Account> Accounts => Set<Account>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Transaction> Transactions => Set<Transaction>();

    public DbSet<Investment> Investments => Set<Investment>();

    public DbSet<Contribution> Contributions => Set<Contribution>();

    public DbSet<FinancialGoal> FinancialGoals => Set<FinancialGoal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}