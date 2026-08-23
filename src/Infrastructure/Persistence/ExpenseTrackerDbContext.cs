using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<ExpenseType> ExpenseTypes => Set<ExpenseType>();
    public DbSet<ExpenseRecord> ExpenseRecords => Set<ExpenseRecord>();
    public DbSet<FixedCost> FixedCosts => Set<FixedCost>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpenseTrackerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
