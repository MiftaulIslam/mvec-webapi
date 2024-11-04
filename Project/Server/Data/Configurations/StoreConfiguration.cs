using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Models;

namespace Server.Data.Configurations;

public class StoreConfiguration:IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        _ = builder.ToTable("Stores");
        _ = builder.HasKey(x => x.Id);
        _ = builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        _ = builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
        // _ = builder.Property(x => x.Logo).IsRequired();
        // _ = builder.Property(x => x.Banner).IsRequired();
        _ = builder.Property(x => x.OpenningHour).IsRequired();
        _ = builder.Property(x => x.ClosingHour).IsRequired();
        _ = builder
            .HasOne<Seller>()
            .WithOne()
            .HasForeignKey<Store>(x => x.SellerId).OnDelete(DeleteBehavior.Cascade);
        _ = builder.HasOne(x => x.Address).WithOne();
    }
}