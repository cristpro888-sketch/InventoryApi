namespace InventoryApi.Models;

public class StockMovement
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty; // "IN" (Entrada) o "OUT" (Salida)
    public int Quantity { get; set; }
    public string Reason { get; set; } = string.Empty; // Ej: "Compra a proveedor", "Venta cliente"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Llaves foráneas para relacionar el movimiento con el Producto y el Usuario
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}