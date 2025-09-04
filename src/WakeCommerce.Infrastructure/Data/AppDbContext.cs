using Microsoft.EntityFrameworkCore;
using WakeCommerce.Infrastructure.Entities;
namespace WakeCommerce.Infrastructure.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) :
DbContext(options)
{
public DbSet<Product> Products => Set<Product>();
3
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
var p = modelBuilder.Entity<Product>();
p.ToTable("Products");
p.HasKey(x => x.Id);
p.Property(x => x.Name).IsRequired().HasMaxLength(200);
p.Property(x => x.Stock).IsRequired();
p.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();
p.ToTable(t =>
{
t.HasCheckConstraint("CK_Products_Price_NonNegative", "Price >=
0");
t.HasCheckConstraint("CK_Products_Stock_NonNegative", "Stock >=
0");
});
modelBuilder.Entity<Product>().HasData(
new Product { Id =
Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Camiseta
Básica", Stock = 50, Price = 39.90m },
new Product { Id =
Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Tênis Conforto",
Stock = 20, Price = 249.90m },
new Product { Id =
Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Calça Jeans",
Stock = 35, Price = 129.99m },
new Product { Id =
Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Mochila Urbana",
Stock = 15, Price = 199.00m },
new Product { Id =
Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Boné Clássico",
Stock = 40, Price = 59.90m }
);
}
}
