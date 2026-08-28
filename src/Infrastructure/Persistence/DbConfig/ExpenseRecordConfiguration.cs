using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.DbConfig;

public sealed class ExpenseRecordConfiguration : IEntityTypeConfiguration<ExpenseRecord>
{
    public void Configure(EntityTypeBuilder<ExpenseRecord> builder)
    {
        builder.ToTable("expense_records");
        builder.HasKey(expense => expense.ExpenseRecordId);

        builder.Property(expense => expense.Date)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.HasIndex(expense => new { expense.UserId, expense.Date })
            .HasDatabaseName("ix_expense_records_user_id_date")
            .HasMethod("btree");

        builder.OwnsOne(expense => expense.AmountExpenses, owned =>
        {
            owned.ToJson("amount_expenses");
            owned.Property(amount => amount.ExpenseTypeId).IsRequired();
            owned.Property(amount => amount.Description).HasMaxLength(500);
            owned.Property(amount => amount.IsActive).HasDefaultValue(true);
            owned.Property(amount => amount.Frequency)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();
            owned.Property(amount => amount.Amount)
                .HasPrecision(18, 2)
                .IsRequired();
        });

        builder.HasOne(expense => expense.User)
            .WithMany(user => user.ExpenseRecords)
            .HasForeignKey(expense => expense.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
