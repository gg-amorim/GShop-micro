using GShop.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GShop.ProductApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Entidade no Singular e tabela no Plural
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Category
        modelBuilder.Entity<Category>()
            .HasKey(c => c.CategoryId);
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(c => c.Category)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                CategoryId = new Guid("d78ade09-7a55-11ee-81d8-0242ac110002"),
                Name = "Material Escolar",
            },
            new Category
            {
                 CategoryId = new Guid("d824837e-7a55-11ee-81d8-0242ac110002"),
                 Name = "Acessórios",
            }
         );
        #endregion

        #region Product
        modelBuilder.Entity<Product>()
           .HasKey(c => c.Id);
        modelBuilder.Entity<Product>()
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Product>()
            .Property(c => c.Description)
            .HasMaxLength(255)
            .IsRequired();
        modelBuilder.Entity<Product>()
            .Property(c => c.ImageURL)
            .HasMaxLength(255)
            .IsRequired();
        modelBuilder.Entity<Product>()
            .Property(c => c.Price)
            .HasPrecision(12, 2);
        #endregion

    }
}
