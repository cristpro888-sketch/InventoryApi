using InventoryApi.Data;
using InventoryApi.Models;
using InventoryApi.Models.DTOs;

namespace InventoryApi.Services;

public class ProductService : IProductService
{
    private readonly InventoryDbContext _context;

    // Inyectamos la base de datos al servicio
    public ProductService(InventoryDbContext context)
    {
        _context = context;
    }

    public List<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public Product? GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public Product CreateProduct(ProductCreateDto dto)
    {
        var newProduct = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            CreatedAt = DateTime.UtcNow
        };

        // Agregamos a la base de datos y guardamos los cambios
        _context.Products.Add(newProduct);
        _context.SaveChanges();
        
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

        // Guardamos los cambios en la base de datos
        _context.SaveChanges();
        
        return existingProduct;
    }

    public bool DeleteProduct(int id)
    {
        var existingProduct = GetProductById(id);
        if (existingProduct == null) return false;

        _context.Products.Remove(existingProduct);
        _context.SaveChanges();
        
        return true;
    }
}