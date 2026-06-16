namespace NinxERP.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public int ComercioId { get; set; }
    public string nomeComercio { get; set; }

}
