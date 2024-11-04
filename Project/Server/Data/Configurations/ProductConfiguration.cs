using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Models;

namespace Server.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        _ = builder.ToTable("Products");

        _ = builder.HasKey(p => p.Id);

        _ = builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        _ = builder.Property(p => p.Description)
            .HasMaxLength(500);

        _ = builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        _ = builder.Property(p => p.PictureUrl)
            .HasMaxLength(200);

        _ = builder.Property(p => p.Category)
            .IsRequired()
            .HasMaxLength(50);

        _ = builder.Property(p => p.Brand)
            .IsRequired()
            .HasMaxLength(50);

        _ = builder.Property(p => p.QuantityInStock)
            .IsRequired();
    }
}
