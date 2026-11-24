using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using InventoryApi.Data;
using InventoryApi.Models;
using InventoryApi.Models.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace InventoryApi.Services;

public class AuthService : IAuthService
{
    private readonly InventoryDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(InventoryDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public User? RegisterUser(UserAuthDto dto)
    {
        // Verificamos si el nombre de usuario ya existe en SQL Server
        if (_context.Users.Any(u => u.Username == dto.Username))
            return null;

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = HashPassword(dto.Password),
            Role = "Admin"
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public string? Login(UserAuthDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username);
        
        // Si no existe o la contraseña encriptada no coincide, rechazamos el acceso
        if (user == null || user.PasswordHash != HashPassword(dto.Password))
            return null;

        // Si las credenciales son válidas, generamos el pase VIP (Token JWT)
        return GenerateJwtToken(user);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private string GenerateJwtToken(User user)
    {
        var jwtKey = _configuration["Jwt:Key"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}