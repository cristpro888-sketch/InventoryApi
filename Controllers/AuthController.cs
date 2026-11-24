using InventoryApi.Models.DTOs;
using InventoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserAuthDto dto)
    {
        var user = _authService.RegisterUser(dto);
        if (user == null)
        {
            return BadRequest(new { message = "El nombre de usuario ya está en uso." });
        }

        return Ok(new { message = "Usuario registrado exitosamente.", username = user.Username });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserAuthDto dto)
    {
        var token = _authService.Login(dto);
        if (token == null)
        {
            return Unauthorized(new { message = "Usuario o contraseña incorrectos." });
        }

        return Ok(new { token });
    }
}