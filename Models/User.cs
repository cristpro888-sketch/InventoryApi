using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Models;

public class User
{

public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty ;

    public string Role { get; set; } = "User";

    public  DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
}