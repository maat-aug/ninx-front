namespace NinxERP.Domain.DTOs;

public class CriarProdutoDTO
{
    public int      ComercioID     { get; set; }
    public int?      CategoriaID    { get; set; }
    public string   Nome           { get; set; } = string.Empty;
    public string?   CodigoBarras   { get; set; } = string.Empty;
    public decimal  PrecoVenda     { get; set; }
    public decimal?  PrecoCusto     { get; set; }
    public string   UnidadeMedida  { get; set; } = string.Empty;
    public DateTime? Validade       { get; set; }
    public int      EstoqueInicial { get; set; }
    public int QuantidadeMinima { get; set; }
}
