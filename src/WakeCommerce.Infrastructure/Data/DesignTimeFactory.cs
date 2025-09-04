using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace WakeCommerce.Infrastructure.Data;
public class DesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
{
public AppDbContext CreateDbContext(string[] args)
{
var options = new DbContextOptionsBuilder<AppDbContext>()
.UseSqlite("Data Source=../WakeCommerce.Api/wakecommerce.db")
4
.Options;
return new AppDbContext(options);
}
}
