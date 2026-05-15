using System.Collections.Generic;

namespace NinxERP.Domain.Entities;

public class UserSession
{
    public User? CurrentUser { get; private set; }
    public Commerce? CurrentCommerce { get; private set; }
    public List<Commerce> AvailableCommerces { get; private set; } = new();
    public string? Token { get; set; }
    
    public bool IsAuthenticated => CurrentUser != null;
    public bool IsAdmin => CurrentUser?.Role == "Admin";
    public bool IsManager => CurrentUser?.Role == "Gerente" || CurrentUser?.Role == "Admin";
    public bool IsEmployee => CurrentUser?.Role == "Funcionario";

    public void Start(User user, List<Commerce> commerces) 
    {
        CurrentUser = user;
        AvailableCommerces = commerces;
        if (commerces.Count > 0) CurrentCommerce = commerces[0];
    }

    public void ChangeCommerce(string commerceId)
    {
        CurrentCommerce = AvailableCommerces.Find(c => c.Id == commerceId);
    }

    public void End() 
    {
        CurrentUser = null;
        CurrentCommerce = null;
        AvailableCommerces.Clear();
        Token = null;
    }
}
