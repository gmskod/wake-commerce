namespace WakeCommerce.Infrastructure.Entities;
public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public void Update(string name, int stock, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new
                ArgumentException("Name is required");
        if (stock < 0)
            throw new ArgumentOutOfRangeException(nameof(stock));
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price));

        Name = name.Trim();
        Stock = stock;
        Price = price;
    }
}