using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Models;

namespace Server.Data.Configurations;

public class AddressConfiguration:IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        _ = builder.ToTable("Address");
        _ = builder.HasKey(x => x.Id);
        _ = builder.Property(x => x.FullAddress).IsRequired();
        _ = builder.Property(x => x.Region).IsRequired();
        _ = builder.Property(x => x.City).IsRequired();
        _ = builder.Property(x => x.Zone).IsRequired();
        _ = builder.HasOne<User>()
            .WithMany(x => x.Address)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        _ = builder
            .HasOne<Store>()
            .WithOne(x => x.Address)
            .HasForeignKey<Address>(x => x.StoreId)
            .OnDelete(DeleteBehavior.SetNull);

    }
}