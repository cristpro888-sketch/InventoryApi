using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Models.DTOs;

public class ProductCreateDto
{
    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "La descripción es demasiado larga.")]
    public string? Description { get; set; }

    [Range(0.01, 10000, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Price { get; set; }

    [Range(0, 10000, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }
}