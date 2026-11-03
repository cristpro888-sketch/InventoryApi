using InventoryApi.Models;
using InventoryApi.Models.DTOs;

namespace InventoryApi.Services;

public class ProductService : IProductService
{
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Gaming Laptop", Description = "High performance laptop", Price = 1200, Stock = 10, CreatedAt = DateTime.UtcNow },
        new Product { Id = 2, Name = "Mechanical Keyboard", Description = "RGB Backlit keyboard", Price = 80, Stock = 25, CreatedAt = DateTime.UtcNow }
    };

    public List<Product> GetAllProducts() => _products;

    public Product? GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Product CreateProduct(ProductCreateDto dto)
    {
        var newProduct = new Product
        {
            Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            CreatedAt = DateTime.UtcNow
        };

        _products.Add(newProduct);
        return newProduct;
    }

    public Product? UpdateProduct(int id, ProductCreateDto dto)
    {
        var existingProduct = GetProductById(id);
        if (existingProduct == null) return null;

        existingProduct.Name = dto.Name;
        existingProduct.Description = dto.Description;
        existingProduct.Price = dto.Price;
        existingProduct.Stock = dto.Stock;

        return existingProduct;
    }

    public bool DeleteProduct(int id)
    {
        var existingProduct = GetProductById(id);
        if (existingProduct == null) return false;

        _products.Remove(existingProduct);
        return true;
    }
}