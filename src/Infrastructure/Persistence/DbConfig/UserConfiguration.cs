using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.DbConfig;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.UserId);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(320);

        builder.HasIndex(user => user.Email)
            .HasDatabaseName("ux_users_email")
            .HasMethod("btree")
            .IsUnique();

        builder.Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasMany(user => user.ExpenseRecords)
            .WithOne(expense => expense.User)
            .HasForeignKey(expense => expense.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.FixedCosts)
            .WithOne(cost => cost.User)
            .HasForeignKey(cost => cost.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
