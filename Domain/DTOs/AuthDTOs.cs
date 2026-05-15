namespace NinxERP.Domain.DTOs
{
    /// <summary>
    /// DTO para requisição de login
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int? ComercioId { get; set; }
    }

    /// <summary>
    /// DTO para resposta de login
    /// Pode conter um token (sucesso) ou uma lista de comércios (seleção necessária)
    /// </summary>
    public class LoginResponse
    {
        public string? Token { get; set; }
        public List<ComercioOption>? Comercios { get; set; }
    }

    /// <summary>
    /// Opção de comércio para seleção
    /// </summary>
    public class ComercioOption
    {
        public int ComercioID { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
