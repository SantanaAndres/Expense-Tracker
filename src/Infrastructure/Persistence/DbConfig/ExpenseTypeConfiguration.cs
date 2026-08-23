using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.DbConfig;

public sealed class ExpenseTypeConfiguration : IEntityTypeConfiguration<ExpenseType>
{
    public void Configure(EntityTypeBuilder<ExpenseType> builder)
    {
        builder.ToTable("expense_types");
        builder.HasKey(type => type.ExpenseTypeId);

        builder.Property(type => type.ExpenseName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(type => type.ExpenseName)
            .HasDatabaseName("ux_expense_types_name")
            .HasMethod("btree")
            .IsUnique();
    }
}
