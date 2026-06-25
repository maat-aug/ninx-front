namespace NinxERP.Domain.DTOs
{
    public class VendaHistoricoDTO
    {
        public int VendaID { get; set; }
        public int ComercioID { get; set; }
        public int UsuarioID { get; set; }
        public decimal Total { get; set; }
        public string TipoVenda { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
        public Guid DocumentoGuid { get; set; } 
    }
}
