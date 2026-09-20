using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.DbConfig;

public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        
        builder.HasIndex(r => r.Id);
        
        builder
            .HasKey(r => r.Id)
            .HasName("refresh_token_pk");

        builder.Property(r => r.Id)
            .HasColumnName("id");

        builder
            .Property(r => r.ExpiryDate)
            .HasColumnName("expiry_date")
            .HasDefaultValue(DateTime.UtcNow.AddDays(30));
        
        builder
            .Property(r => r.Token)
            .HasColumnName("token")
            .IsRequired();
        
        builder
            .Property(r => r.IsRevoked)
            .HasColumnName("is_revoked")
            .HasDefaultValue(false);
        
        builder
            .Property(r => r.CreatedDate)
            .HasColumnName("created_date")
            .HasDefaultValueSql("GETDATE()");
    }
}