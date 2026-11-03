using InventoryApi.Models;
using InventoryApi.Models.DTOs;

namespace InventoryApi.Services;

public interface IProductService
{
    List<Product> GetAllProducts();
    Product? GetProductById(int id);
    Product CreateProduct(ProductCreateDto dto);
    Product? UpdateProduct(int id, ProductCreateDto dto);
    bool DeleteProduct(int id);
}