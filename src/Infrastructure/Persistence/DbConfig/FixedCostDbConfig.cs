using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.DbConfig;

public sealed class FixedCostDbConfig : IEntityTypeConfiguration<FixedCost>
{
    public void Configure(EntityTypeBuilder<FixedCost> builder)
    {
        builder.ToTable("fixed_costs");
        builder.HasKey(cost => cost.FixedCostId)
            .HasName("fixed_cost_pk");

        builder.Property(f => f.FixedCostId)
            .HasColumnName("fixed_cost_id");

        builder.HasIndex(cost => cost.UserId)
            .HasDatabaseName("ix_fixed_costs_user_id")
            .HasMethod("btree");
        
        builder
            .Property(f => f.UserId)
            .HasColumnName("user_id");
        
        builder.OwnsMany(cost => cost.AmountExpenses, owned =>
        {
            owned.ToJson("amount_expenses");
            owned.Property(amount => amount.ExpenseTypeId).IsRequired();
            owned.Property(amount => amount.Description).HasMaxLength(500);
            owned.Property(amount => amount.FrequencyEnum)
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
