using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.DbConfig;

public sealed class FixedCostDbConfig : IEntityTypeConfiguration<FixedCost>
{
    public void Configure(EntityTypeBuilder<FixedCost> builder)
    {
        builder.ToTable("fixed_costs");
        builder.HasKey(cost => cost.FixedCostId);

        builder.HasIndex(cost => cost.UserId)
            .HasDatabaseName("ix_fixed_costs_user_id")
            .HasMethod("btree");

        builder.OwnsMany(cost => cost.AmountExpenses, owned =>
        {
            owned.ToJson("amount_expenses");
            owned.Property(amount => amount.ExpenseTypeId).IsRequired();
            owned.Property(amount => amount.IsActive).HasDefaultValue(true);
            owned.Property(amount => amount.Description).HasMaxLength(500);
            owned.Property(amount => amount.Frequency)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();
            owned.Property(amount => amount.Amount)
                .HasPrecision(18, 2)
                .IsRequired();
        });

        builder.HasOne(cost => cost.User)
            .WithMany(user => user.FixedCosts)
            .HasForeignKey(cost => cost.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
