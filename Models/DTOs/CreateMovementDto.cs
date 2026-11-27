using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Models.DTOs;

public class CreateMovementDto
{
    [Required(ErrorMessage = "El ID del producto es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del producto debe ser válido.")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "El tipo de movimiento es obligatorio.")]
    [RegularExpression("^(IN|OUT)$", ErrorMessage = "El tipo debe ser 'IN' (Entrada) o 'OUT' (Salida).")]
    public string Type { get; set; } = string.Empty;

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "El motivo es obligatorio.")]
    [MaxLength(200, ErrorMessage = "El motivo no puede superar los 200 caracteres.")]
    public string Reason { get; set; } = string.Empty;
}