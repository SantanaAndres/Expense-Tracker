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

        builder
            .Property(e => e.ExpenseTypeId)
            .HasColumnName("expense_type_id");
        
        builder.Property(type => type.ExpenseName)
            .IsRequired()
            .HasColumnName("expense_name")
            .HasMaxLength(100);
        
        builder.Property(type => type.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(type => type.ExpenseName)
            .HasDatabaseName("ux_expense_types_name")
            .HasMethod("btree")
            .IsUnique();
    }
}
