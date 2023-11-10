using GShop.CartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GShop.CartApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product>? Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       #region Product
        modelBuilder.Entity<Product>()
           .HasKey(c => c.Id);
        modelBuilder.Entity<Product>()
            .Property(c => c.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Product>().
           Property(c => c.Name).
             HasMaxLength(100).
               IsRequired();

        modelBuilder.Entity<Product>().
          Property(c => c.Description).
               HasMaxLength(255).
                   IsRequired();

        modelBuilder.Entity<Product>().
          Property(c => c.ImageURL).
              HasMaxLength(255).
                  IsRequired();

        modelBuilder.Entity<Product>().
           Property(c => c.CategoryName).
               HasMaxLength(100).
                IsRequired();

        modelBuilder.Entity<Product>().
           Property(c => c.Price).
             HasPrecision(12, 2);
        #endregion

        #region CartHeader
        modelBuilder.Entity<CartHeader>().
             Property(c => c.UserId)
             .IsRequired();

        modelBuilder.Entity<CartHeader>().
           Property(c => c.CouponCode).
              HasMaxLength(100);

        #endregion

    }
}
