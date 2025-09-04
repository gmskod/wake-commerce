using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WakeCommerce.Infrastructure.Data;

public class WakeCommerceDbContextFactory : IDesignTimeDbContextFactory<WakeCommerceDbContext>
{
    public WakeCommerceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WakeCommerceDbContext>();
        optionsBuilder.UseSqlite("Data Source=../WakeCommerce.Api/wakecommerce.db");

        return new WakeCommerceDbContext(optionsBuilder.Options);
    }
}
