using Microsoft.EntityFrameworkCore;
using WakeCommerce.Infrastructure.Data;
using WakeCommerce.Infrastructure.Entities;
using WakeCommerce.Infrastructure.Repositories;
using WakeCommerce.Infrastructure.Seed;
var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("Default") ?? "Data
Source=wakecommerce.db";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(cs));
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
6
app.UseSwagger();
app.UseSwaggerUI();
}
await app.MigrateAndSeedAsync();
record CreateProductDto(string Name, int Stock, decimal Price);
record UpdateProductDto(string Name, int Stock, decimal Price);
record ProductResponse(Guid Id, string Name, int Stock, decimal Price);
var group = app.MapGroup("/products").WithTags("Products");
group.MapPost("/", async (CreateProductDto dto, IProductRepository repo,
CancellationToken ct) =>
{
if (string.IsNullOrWhiteSpace(dto.Name)) return Results.BadRequest("Name
is required");
if (dto.Stock < 0) return Results.BadRequest("Stock cannot be negative");
if (dto.Price < 0) return Results.BadRequest("Price cannot be negative");
var p = new Product { Name = dto.Name.Trim(), Stock = dto.Stock, Price =
dto.Price };
await repo.AddAsync(p, ct);
await repo.SaveChangesAsync(ct);
return Results.Created($"/products/{p.Id}", new ProductResponse(p.Id,
p.Name, p.Stock, p.Price));
});
// Busca por nome, ordenação e paginação
// ?search=camisa&sortBy=price|stock|name&sortDir=asc|desc&page=1&pageSize=20
group.MapGet("/", async (
string? search, string? sortBy, string? sortDir, int page = 1, int
pageSize = 20,
IProductRepository repo, CancellationToken ct) =>
{
var q = repo.Query();
if (!string.IsNullOrWhiteSpace(search))
{
var s = search.Trim().ToLower();
q = q.Where(p => p.Name.ToLower().Contains(s));
}
q = (sortBy?.ToLower(), sortDir?.ToLower()) switch
{
("price", "desc") => q.OrderByDescending(p => p.Price),
("price", _) => q.OrderBy(p => p.Price),
("stock", "desc") => q.OrderByDescending(p => p.Stock),
("stock", _) => q.OrderBy(p => p.Stock),
("name", "desc") => q.OrderByDescending(p => p.Name),
7
_ => q.OrderBy(p => p.Name)
};
page = Math.Max(1, page);
pageSize = Math.Clamp(pageSize, 1, 100);
var total = await q.CountAsync(ct);
var items = await q.Skip((page - 1) * pageSize).Take(pageSize)
.Select(p => new ProductResponse(p.Id, p.Name, p.Stock, p.Price))
.ToListAsync(ct);
return Results.Ok(new { total, page, pageSize, items });
});
group.MapGet("/{id:guid}", async (Guid id, IProductRepository repo,
CancellationToken ct) =>
{
var p = await repo.GetAsync(id, ct);
return p is null ? Results.NotFound() : Results.Ok(new
ProductResponse(p.Id, p.Name, p.Stock, p.Price));
});
group.MapPut("/{id:guid}", async (Guid id, UpdateProductDto dto,
IProductRepository repo, CancellationToken ct) =>
{
if (string.IsNullOrWhiteSpace(dto.Name)) return Results.BadRequest("Name
is required");
if (dto.Stock < 0) return Results.BadRequest("Stock cannot be negative");
if (dto.Price < 0) return Results.BadRequest("Price cannot be negative");
var existing = await repo.GetAsync(id, ct);
if (existing is null) return Results.NotFound();
existing.Update(dto.Name, dto.Stock, dto.Price);
await repo.UpdateAsync(existing, ct);
await repo.SaveChangesAsync(ct);
return Results.Ok(new ProductResponse(existing.Id, existing.Name,
existing.Stock, existing.Price));
});
group.MapDelete("/{id:guid}", async (Guid id, IProductRepository repo,
CancellationToken ct) =>
{
var existing = await repo.GetAsync(id, ct);
if (existing is null) return Results.NotFound();
await repo.DeleteAsync(existing, ct);
await repo.SaveChangesAsync(ct);
return Results.NoContent();
8
});
app.Run();
// Necessário para testes de integração
public partial class Program { }