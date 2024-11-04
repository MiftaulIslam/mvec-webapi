using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Models;

namespace Server.Data.Configurations;

public class SellerConfiguration:IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        _ = builder.ToTable("Sellers");
        _ = builder.HasKey(x => x.Id);
        _ = builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        _ = builder.Property(x => x.Email).IsRequired();
        _ = builder.Property(x => x.Phone).IsRequired().HasMaxLength(11);
        _ = builder.Property(x => x.Role).IsRequired();
        _ = builder.HasOne<Store>()
            .WithOne()
            .HasForeignKey<Seller>(x => x.StoreId);

    }
}