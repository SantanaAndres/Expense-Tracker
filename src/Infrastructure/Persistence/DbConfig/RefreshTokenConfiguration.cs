using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.DbConfig;

public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");
        
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("id");

        builder.Property(r => r.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder
            .Property(r => r.Token)
            .HasColumnName("token")
            .IsRequired();

        builder.HasIndex(r => r.Token)
            .IsUnique()
            .HasDatabaseName("ux_refresh_tokens_token");

        builder
            .Property(r => r.ExpiryDate)
            .HasColumnName("expiry_date")
            .HasColumnType("timestamp with time zone")
            .IsRequired();
        
        builder
            .Property(r => r.IsRevoked)
            .HasColumnName("is_revoked")
            .HasDefaultValue(false);
        
        builder
            .Property(r => r.CreatedDate)
            .HasColumnName("created_date")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()");

        builder.HasOne(r => r.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}