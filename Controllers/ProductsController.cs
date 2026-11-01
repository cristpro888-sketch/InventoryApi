using InventoryApi.Models;
using InventoryApi.Models.DTOs;
using InventoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    // Dependency Injection via constructor
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/products
    [HttpGet]
    public ActionResult<List<Product>> GetAll()
    {
        var products = _productService.GetAllProducts();
        return Ok(products);
    }

    // GET: api/products/1
    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound(new { message = $"Product with ID {id} was not found." });
        }
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public ActionResult<Product> Create([FromBody] ProductCreateDto productDto)
    {
        var newProduct = _productService.CreateProduct(productDto);
        return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
    }
}