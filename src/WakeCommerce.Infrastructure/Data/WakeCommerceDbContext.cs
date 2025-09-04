using Microsoft.EntityFrameworkCore;
using WakeCommerce.Domain.Models;

namespace WakeCommerce.Infrastructure.Data;

public class WakeCommerceDbContext : DbContext
{
    public WakeCommerceDbContext(DbContextOptions<WakeCommerceDbContext> options)
        : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }

}