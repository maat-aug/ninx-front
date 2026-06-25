namespace NinxERP.Domain.DTOs
{
    public class ClienteDTO
    {
        public int ClienteID { get; set; }
        public int ComercioID { get; set; }
        public string Nome { get; set; } = null!;
        public string? Telefone { get; set; }
        public decimal? LimiteCredito { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public string ComercioNome { get; set; } = null!;
    }
}
