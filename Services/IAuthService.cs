using InventoryApi.Models.DTOs;
using InventoryApi.Models;

namespace InventoryApi.Services;

public interface IAuthService
{
    User? RegisterUser(UserAuthDto dto);
    string? Login(UserAuthDto dto);
}