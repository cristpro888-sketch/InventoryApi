using InventoryApi.Models;
using InventoryApi.Models.DTOs;

namespace InventoryApi.Services;

public interface IProductService
{
    List<Product> GetAllProducts();
    Product? GetProductById(int id);
    Product CreateProduct(ProductCreateDto productDto);
}