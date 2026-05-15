using NinxERP.Domain.DTOs;

namespace NinxERP.Domain.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string Message, LoginResponse? Response)> AuthenticateAsync(string email, string password, int? comercioId = null);
}
