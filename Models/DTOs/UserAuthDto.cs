using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Models.DTOs;

public class UserAuthDto
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [MinLength(4, ErrorMessage = "El usuario debe tener al menos 4 caracteres.")]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string Password { get; set; } = string.Empty;
}