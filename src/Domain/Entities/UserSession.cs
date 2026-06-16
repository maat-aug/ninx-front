using System.Collections.Generic;

namespace NinxERP.Domain.Entities;

public class UserSession
{
    public User? CurrentUser { get; private set; }
    public string? CurrentCommerceId { get; private set; }
    public string? CurrentCommerceName { get; private set; }
    public List<Commerce> AvailableCommerces { get; private set; } = new();
    public string? Token { get; set; }
    
    public bool IsAuthenticated => CurrentUser != null;
    public bool IsAdmin => CurrentUser?.Role?.Equals("Administrador", StringComparison.OrdinalIgnoreCase) ?? false;
    public bool IsManager => IsAdmin || (CurrentUser?.Role?.Equals("Gerente", StringComparison.OrdinalIgnoreCase) ?? false);
    public bool IsEmployee => CurrentUser?.Role?.Equals("Funcionario", StringComparison.OrdinalIgnoreCase) ?? false;

    public void Start(User user, List<Commerce> commerces, string CurrentCommerceID, string nomeComercio) 
    {
        CurrentUser = user;
        AvailableCommerces = commerces;
        CurrentCommerceId = CurrentCommerceID;
        CurrentCommerceName = nomeComercio;
    }
    public void End() 
    {
        CurrentUser = null;
        CurrentCommerceId = null;
        CurrentCommerceName = null;
        AvailableCommerces.Clear();
        Token = null;
    }
}
