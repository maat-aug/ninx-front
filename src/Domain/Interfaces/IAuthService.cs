using NinxERP.Domain.DTOs;
using NinxERP.Domain.Entities;

namespace NinxERP.Domain.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string Message, LoginResponse? Response)> AuthenticateAsync(string email, string password, int? comercioId = null);
    Task<bool> TrocarComercioAsync(int comercioId);
    Task<List<Commerce>> GetComerciosUsuarioAsync(int usuarioId);
}
