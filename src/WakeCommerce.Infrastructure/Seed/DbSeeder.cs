using Microsoft.EntityFrameworkCore;
using WakeCommerce.Infrastructure.Data;
namespace WakeCommerce.Infrastructure.Seed;
public static class DbSeeder
{
public static async Task MigrateAndSeedAsync(this IHost app)
{
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await db.Database.MigrateAsync();
// Seed via HasData
}
}
