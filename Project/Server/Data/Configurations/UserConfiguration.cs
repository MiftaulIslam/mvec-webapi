using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Models;

namespace Server.Data.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
         _ = builder.ToTable("Users");
         _ = builder.HasKey(x => x.Id);
         _ = builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
         _ = builder.Property(x => x.Email).IsRequired();
         _ = builder.HasIndex(x => x.Email).IsUnique();
         _ = builder.Property(x => x.Phone).HasMaxLength(11);
         _ = builder.Property(x => x.Role).IsRequired();    
         _ = builder.HasMany(a => a.Address);

    }
}