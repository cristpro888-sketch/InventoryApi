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

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<List<Product>> GetAll()
    {
        return Ok(_productService.GetAllProducts());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create([FromBody] ProductCreateDto dto)
    {
        var newProduct = _productService.CreateProduct(dto);
        return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] ProductCreateDto dto)
    {
        var updatedProduct = _productService.UpdateProduct(id, dto);
        if (updatedProduct == null) return NotFound();

        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _productService.DeleteProduct(id);
        if (!isDeleted) return NotFound();

        return NoContent();
    }
}