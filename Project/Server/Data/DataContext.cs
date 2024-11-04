using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Data.Configurations;
using Server.Entities.Models;
using Server.Entities.Models.Auth;

namespace Server.Data;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.ApplyConfiguration(new ProductConfiguration());
        _ = modelBuilder.ApplyConfiguration(new UserConfiguration());
        _ = modelBuilder.ApplyConfiguration(new SellerConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AddressConfiguration());
        _ = modelBuilder.ApplyConfiguration(new StoreConfiguration());

    }
}
