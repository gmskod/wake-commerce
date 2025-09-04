using Microsoft.EntityFrameworkCore;
using WakeCommerce.Infrastructure.Data;
using WakeCommerce.Infrastructure.Interface;
using WakeCommerce.Infrastructure.Repositories;
using WakeCommerce.Infrastructure.Seed;
using WakeCommerce.Service;
using WakeCommerce.Service.Interfaces;
using WakeCommerce.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WakeCommerceDbContext>(options =>
    options.UseInMemoryDatabase("WakeCommerceDB"));

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "WakeCommerce API",
        Version = "v1",
        Description = "API para gerenciamento de produtos do WakeCommerce",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Guilherme Sales",
            Email = "guilhermemustafe@hotmail.com"
        }
    });

    var xmlFile = "WakeCommerce.Api.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WakeCommerce API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.MigrateAndSeedAsync();

app.Run();
