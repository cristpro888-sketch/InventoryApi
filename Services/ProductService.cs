using InventoryApi.Models;
using InventoryApi.Models.DTOs;

namespace InventoryApi.Services;

public class ProductService : IProductService
{
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Gaming Laptop", Description = "High performance laptop", Price = 1200.00m, Stock = 10 },
        new Product { Id = 2, Name = "Mechanical Keyboard", Description = "RGB Backlit keyboard", Price = 80.00m, Stock = 25 }
    };

    public List<Product> GetAllProducts() => _products;

    public Product? GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Product CreateProduct(ProductCreateDto productDto)
    {
        var newProduct = new Product
        {
            Id = _products.Count + 1,
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Stock = productDto.Stock,
            CreatedAt = DateTime.UtcNow
        };

        _products.Add(newProduct);
        return newProduct;
    }
}